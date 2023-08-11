using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CalifornianHealthMonolithic.Shared
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly string _tokenSecretKey;
        private readonly int _tokenExpirationMinutes;
        private readonly SignInManager<Patient> _signInManager;
        private readonly UserManager<Patient> _userManager;
        public AuthenticationService(IConfiguration configuration, SignInManager<Patient> signInManager, UserManager<Patient> userManager)
        {
            _configuration = configuration;
            _tokenSecretKey = configuration["JwtSettings:SecretKey"];
            _tokenExpirationMinutes = int.Parse(configuration["JwtSettings:TokenExpirationMinutes"]);
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public string GenerateAccessToken(Patient user)
        {
            // Prepare claims for token
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JwtSettings:Issuer"])
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSecretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            // Create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]))
            };
            try
            {
                // Validate the token and extract its claims
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                return true; // The token is valid
            }
            catch (SecurityTokenException)
            {
                return false; // Token validation failed
            }
        }
        public async Task<string> GetValidTokenAsync(ClaimsPrincipal userPrincipal)
        {
            // Get current logged user
            var user = await _userManager.GetUserAsync(userPrincipal);

            // Get latest access token issued for user in claims
            var accessTokenClaim = userPrincipal.Claims.LastOrDefault(c => c.Type == "access_token");

            // If valid token doesn't exist, create a new one
            if (accessTokenClaim == null || !ValidateToken(accessTokenClaim.Value))
            {
                // Remove current claims
                var currentClaims = await _signInManager.UserManager.GetClaimsAsync(user);
                await _signInManager.UserManager.RemoveClaimsAsync(user, currentClaims);
                // Generate new access token
                var accessToken = GenerateAccessToken(user);
                // Create a list of claims to add to the user's ClaimsIdentity
                var claims = new List<Claim>
                {
                    new Claim("access_token", accessToken)
                };
                // Add claims and refresh sign in
                await _signInManager.UserManager.AddClaimsAsync(user, claims);
                await _signInManager.RefreshSignInAsync(user);
                // Return new token
                return accessToken;
            }

            // Returns valid access token
            return accessTokenClaim.Value;
        }
    }
}