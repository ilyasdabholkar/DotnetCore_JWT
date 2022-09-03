using JwtImplementation.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace JwtImplementation.Services
{
    public class TokenGeneratorService : ITokenGenerator
    {

        private readonly IConfiguration _configuration;

        public TokenGeneratorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int id, string email, string role)
        {
            var payload = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,email),
                new Claim(ClaimTypes.Role,role)
            };

            string JwtSecret = _configuration.GetValue<string>("JwtConfig:Secret");
            string JwtIssuer = _configuration.GetValue<string>("JwtConfig:Issuer");
            var JwtAudiance = _configuration.GetValue<string>("JwtConfig:Audiance");

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));

            var jwtToken = new JwtSecurityToken(
                issuer: JwtIssuer,
                audience: JwtAudiance,
                claims: payload,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(symmetricKey,SecurityAlgorithms.HmacSha256)
            );
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return jwtSecurityTokenHandler;
        }
    }
}
