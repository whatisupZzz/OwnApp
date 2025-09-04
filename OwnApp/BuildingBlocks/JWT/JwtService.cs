using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.JWT
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }

    public interface IJwtService
    {
        string GenerateToken(string userId, string phoneNumber);
        ClaimsPrincipal? ValidateToken(string token);
        string? GetUserId(ClaimsPrincipal user);

        string GenerateRefreshToken(string userId);
        bool ValidateRefreshToken(string userId, string refreshToken);
    }

    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;
        private readonly byte[] _key;

        // 简单内存存储 Refresh Token（实际可用 Redis/DB）
        private static readonly List<RefreshToken> _refreshTokens = new();

        public JwtService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
            _key = Encoding.UTF8.GetBytes(_settings.SecretKey);
        }

        public string GenerateToken(string userId, string phoneNumber)
        {
            var claims = new[]
            {
            new Claim("sub", userId),
            new Claim("Phone", phoneNumber),
        };

            var creds = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_settings.ExpirationDays),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _settings.Issuer,
                    ValidAudience = _settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public string? GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirstValue(JwtRegisteredClaimNames.Sub);
        }

        // ----------------- Refresh Token -----------------
        public string GenerateRefreshToken(string userId)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var refreshToken = new RefreshToken
            {
                Token = token,
                UserId = userId,
                Expiration = DateTime.Now.AddDays(60) // 可配置
            };

            // 移除旧的 refresh token
            _refreshTokens.RemoveAll(x => x.UserId == userId);
            _refreshTokens.Add(refreshToken);

            return token;
        }

        public bool ValidateRefreshToken(string userId, string refreshToken)
        {
            var token = _refreshTokens.FirstOrDefault(x => x.UserId == userId && x.Token == refreshToken);
            if (token == null) return false;
            if (token.Expiration < DateTime.Now)
            {
                _refreshTokens.Remove(token);
                return false;
            }
            return true;
        }
    }


}
