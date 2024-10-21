using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentalCar.Security.Core.Configs;
using RentalCar.Security.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentalCar.Security.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly JwtConfig _jwtConfig;

        public LoginService(IOptions<JwtConfig> options)
        {
            _jwtConfig = options.Value;
        }

        public string? GenerateJwtToken(string name, string email, string role)
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

        /*
        public string CreateToken(User user)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserID),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            // Add more claims as needed
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        Issuer = _configuration["Jwt:Issuer"], // Add this line
        Audience = _configuration["Jwt:Audience"] 
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}
         */ 
    }
}
