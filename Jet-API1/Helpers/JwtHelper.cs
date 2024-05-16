
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jet_API1.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;
        public JwtHelper ( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string secureKey = "BEEABD3A-78C9-4E55-813D-642D25944B8F";
        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            if (string.IsNullOrEmpty(secureKey))
            {
                throw new InvalidOperationException("JWT secret is missing or empty.");
            }



            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
