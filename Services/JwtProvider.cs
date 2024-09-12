using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using COLOR.Domain.Entities;
using COLOR.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace COLOR.Services;

public class JwtProvider : IJwtProvider
{
    private readonly IOptions<JwtOptions> _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options;
    }
    
    public string GenerateToken(UserEntity user)
    {
        Claim[] claims = [new("userId", user.Id.ToString())];
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey)), 
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.Value.ExpiresHours));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}