using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtConfig _jwtConfig;

    public JwtTokenService(IOptions<JwtConfig> options)
    {
        _jwtConfig = options.Value;
    }
    
    public string GenerateJwtToken(string name, string email, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var securityToken = new SecurityTokenDescriptor
        {
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            Expires = DateTime.Now.AddMinutes(_jwtConfig.DurationInMinutes),
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Email, email),
                //new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            ])
        };

        var tokenhandler = new JwtSecurityTokenHandler();

        var token = tokenhandler.CreateToken(securityToken);

        return tokenhandler.WriteToken(token);
    }
}