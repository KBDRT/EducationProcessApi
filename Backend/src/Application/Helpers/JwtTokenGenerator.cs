using Application.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Helpers
{
    public class JwtTokenGenerator
    {
        private readonly IOptions<AuthSettings> _settings;

        public JwtTokenGenerator(IOptions<AuthSettings> settings)
        {
            _settings = settings;
        }

        public string GetNewJwtTokenString(List<Claim> claims)
        {

            var key = _settings.Value.SecretKey;
            var jwtToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(_settings.Value.TokenLifeTime),
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256
                ));

            var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return jwtTokenString;
        }
    }
}
