using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CalifornianHealthMonolithic.Shared
{
    public interface IAuthenticationService
    {
        public string GenerateAccessToken(Patient user);
        public bool ValidateToken(string token);
        public Task<string> GetValidTokenAsync(ClaimsPrincipal userPrincipal);
    }
}