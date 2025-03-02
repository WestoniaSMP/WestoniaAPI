using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WestoniaAPI.Core;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.DataLogic
{
    public class AuthService(IConfiguration configuration) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateJwtToken(WestoniaUser user)
        {
            byte[] derivedKey = new byte[32];

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] jwtEncryptionKeyBytes = Encoding.UTF8.GetBytes(_configuration["JwtSettings:EncryptionKey"] ?? throw new Exception("JWT key not found"));
                derivedKey = sha256.ComputeHash(jwtEncryptionKeyBytes);
            }

            string jwtIssuer = _configuration["JwtSettings:Issuer"] ?? string.Empty;
            string jwtAudience = _configuration["JwtSettings:Audience"] ?? string.Empty;

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("UserName", user.UserName ?? throw new Exception("User name not found")),
            new Claim("DiscordId", user.DiscordId),
            new Claim("MinecraftUuid", user.MinecraftUuid.ToString()),
            new Claim("Email", user.Email ?? throw new Exception("User email not found")),
            new Claim("Language", user.Language),
            };


            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(derivedKey), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsIdentity GenerateClaims(WestoniaUser user)
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.MinecraftUuid.ToString()),

            });

            //claims.AddClaim(new Claim(ClaimTypes.Role, user.Role));

            return claims;
        }
    }
}
