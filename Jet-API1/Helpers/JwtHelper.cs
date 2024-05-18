using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jet_API1.Helpers;

public class JwtHelper
{
    private readonly IConfiguration _configuration;
    private readonly string _secureKey;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _secureKey = _configuration["JWT:SecretKey"];

        if (string.IsNullOrEmpty(_secureKey))
        {
            throw new InvalidOperationException("JWT secret key is missing or empty.");
        }
    }

    public JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
}
