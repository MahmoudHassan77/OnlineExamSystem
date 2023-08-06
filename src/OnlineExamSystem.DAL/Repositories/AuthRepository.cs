using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using OnlineExamSystem.Common.Contracts.Repositories;
using OnlineExamSystem.Common.Dtos;
using OnlineExamSystem.Common.Helpers;
using OnlineExamSystem.Data;
using OnlineExamSystem.Domain.Entities;
using OnlineExamSystem.Domain.Identity;
using System.Data;
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
    readonly JWT _jwt;

    public AuthRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext context, IOptions<JWT> jwt)
    {
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
        _jwt = jwt.Value;
    }
    public async Task<AuthResponse> Login(LoginDto cred)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(cred.Email);
            var userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = await createUserClaims(user, userRoles);
            TokenResponse token = AuthHelper.CreateToken(claims, _jwt);
            string refreshToken = AuthHelper.GetRefreshToken();
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
    }

    public async Task<AuthResponse> Register(CreateUserDto user, string role)
    {
        try
        {
            
            ApplicationUser newUser = new ApplicationUser
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.Username.ToLower(),
                Age = user.Age,
            };
            var userAdded = await _userManager.CreateAsync(newUser, user.Password);
            if (!userAdded.Succeeded) throw new Exception("User registration is failed."); 
            var newUserAdded = await _userManager.FindByEmailAsync(user.Email);
            var userRoles = await _userManager.GetRolesAsync(newUserAdded);
            List<Claim> claims = await createUserClaims(newUserAdded, userRoles);
            TokenResponse token = AuthHelper.CreateToken(claims, _jwt);
            string refreshToken = AuthHelper.GetRefreshToken();
            var tokenInfo = _context.TokenInfos.FirstOrDefault(a => a.Username == newUserAdded.UserName);
            if (tokenInfo == null)
            {
                var info = new TokenInfo
                {
                    Username = newUserAdded.UserName,
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
            return new AuthResponse("User is registered Successfully", null, true, user.Name, newUserAdded.UserName, token.TokenString, refreshToken, token.ValidTo);
        }
        catch (Exception e)
        {
            return new AuthResponse(e.Message, new List<string>(), false, null, null, null, null,null);
        }
    }

    private async Task<List<Claim>> createUserClaims(ApplicationUser user, IList<string>? userRoles)
    {
        var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

        foreach (var userRole in userRoles!)
            authClaims.Add(new Claim("Role", userRole));

        var userClaims = await _userManager.GetClaimsAsync(user);

        return authClaims.Union(userClaims).ToList();
    }
}
