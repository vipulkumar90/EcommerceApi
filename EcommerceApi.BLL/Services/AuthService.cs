using EcommerceApi.BLL.Models;
using EcommerceApi.BLL.Services.IServices;
using EcommerceApi.DAL.Entities.User;
using EcommerceApi.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApi.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly IOptions<JwtOptions> jwtOption;
        private User user;
        public AuthService(UserManager<User> userManager, IOptions<JwtOptions> jwtOption)
        {
            this.userManager = userManager;
            this.jwtOption = jwtOption;
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(token);
            return tokenString;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var expiration = DateTime.UtcNow.AddHours(jwtOption.Value.LifeTime);

            var token = new JwtSecurityToken(
                issuer: jwtOption.Value.Issuer,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, user.UserName)
             };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = jwtOption.Value.Key;
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public async Task<bool> ValidateUser(LogInDto logInDto)
        {
            user = await userManager.FindByNameAsync(logInDto.Email);
            var validPassword = await userManager.CheckPasswordAsync(user, logInDto.Password);
            return (user != null && validPassword);
        }
    }
}
