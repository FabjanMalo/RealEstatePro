using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstatePro.Application.Abstractions.Contracts.AuthService;
using RealEstatePro.Application.Abstractions.Database;
using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure.Contracts.AuthService;

public class AuthManager(
    IConfiguration _configuration,
    IApplicationContext _context
    )
    : IAuthManager
{
    public async Task<string> CreateToken(User user)
    {
        var signingCredentials = GetSigningCredentials();

        var claims = await GetClaims(user);

        var tokenOption = GenerazeTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOption);
    }



    private JwtSecurityToken GenerazeTokenOptions
        (SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");

        var expiration = DateTime.Now.AddSeconds(Convert.ToDouble(jwtSettings.
            GetSection("lifetime").Value));

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expiration,
            signingCredentials: signingCredentials
            );
        return token;

    }
    private SigningCredentials GetSigningCredentials()
    {
        var key = _configuration.GetSection("JwtConfig").GetSection("SecretKey").Value;

        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(User user)
    {

        var userRole = await _context.UserRoles.FirstOrDefaultAsync(u => u.Id == user.UserRoleId);

        var claims = new List<Claim>
            {
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Role, userRole.Name)
        };

        return claims;
    }
}
