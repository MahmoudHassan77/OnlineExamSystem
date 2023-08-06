
using Microsoft.IdentityModel.Tokens;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Domain.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineExamSystem.Common.Helpers;
public static class AuthHelper
{
    public static string GetRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public static TokenResponse CreateToken(List<Claim> claims, JWT jwt)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret));
        var signingCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
                issuer: jwt.ValidIssuer,
                audience: jwt.ValidAudience,
                expires: DateTime.Now.AddDays(jwt.DurationDays),
                claims: claims,
                signingCredentials: signingCredentials
            );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenResponse(tokenString, token.ValidTo);
    }
}
