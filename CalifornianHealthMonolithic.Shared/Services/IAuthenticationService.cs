using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CalifornianHealthMonolithic.Shared
{
    public interface IAuthenticationService
    {
        /// <summary>Generate new access token for specified user</summary>
        /// <param name="user">User for which access token will be created</param>
        /// <returns>Return a new valid JWT token</returns>
        public string GenerateAccessToken(Patient user);
        /// <summary>Checks if the provided token is valid</summary>
        /// <param name="token">JWT token to validate</param>
        /// <returns>Return true if token is valid</returns>
        public bool ValidateToken(string token);
        /// <summary>Checks if a valid token exists and returns it. If not valid, a new token will be issued</summary>
        /// <param name="userPrincipal">ClaimsPrincipal of the current logged user</param>
        /// <returns>Returns a valid JWT access token</returns>
        public Task<string> GetValidTokenAsync(ClaimsPrincipal userPrincipal);
    }
}