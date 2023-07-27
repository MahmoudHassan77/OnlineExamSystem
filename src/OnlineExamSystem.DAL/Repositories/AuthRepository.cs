using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Data;
using OnlineExamSystem.Domain.Entities;
using OnlineExamSystem.Domain.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineExamSystem.DAL.Repositories;
public class AuthRepository : IAuthRepository
{
    readonly UserManager<ApplicationUser> _userManager;
    readonly IConfiguration _configuration;
    readonly ApplicationDbContext _context;

    public AuthRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }
    public async Task<AuthResponse> Login(LoginDto cred)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(cred.Email);
            var userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = createUserClaims(user, userRoles);
            TokenResponse token = createToken(claims);
            string refreshToken = getRefreshToken();
            var tokenInfo = _context.TokenInfos.FirstOrDefault(a => a.Username == user.UserName);
            if (tokenInfo == null)
            {
                var info = new TokenInfo
                {
                    Username = user.UserName,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiry = DateTime.Now.AddDays(1)
                };
                _context.TokenInfos.Add(info);
            }
            else
            {
                tokenInfo.RefreshToken = refreshToken;
                tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(1);
            }
            _context.SaveChanges();
            return new AuthResponse("User is login Successfully", null, true, user.Name, user.UserName, token.TokenString, refreshToken, token.ValidTo);
        }
        catch(Exception e) {
            return new AuthResponse(e.Message, new List<string>(), false, "", "", "", "", DateTime.Now);
        }


        return null;
    }

    private string getRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private TokenResponse createToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenResponse (tokenString, token.ValidTo );
    }

    private List<Claim> createUserClaims(ApplicationUser user, IList<string>? userRoles)
    {
        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
        foreach (var userRole in userRoles!)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        return authClaims;
    }
}
