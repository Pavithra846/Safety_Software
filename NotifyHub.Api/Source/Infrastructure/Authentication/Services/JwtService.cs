using Microsoft.IdentityModel.Tokens;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace NotifyHub.Api.Source.Infrastructure.Authentication.Services
{
    public class JwtService: IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Users user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
             
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key"))
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_config["Jwt:DurationInMinutes"])
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
}
