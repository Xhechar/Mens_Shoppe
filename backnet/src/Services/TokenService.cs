
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class TokenService : ITokenService
{
  private readonly IConfiguration configuration;

  public TokenService(IConfiguration configuration)
  {
    this.configuration = configuration;
  }

  public string GenerateToken(TokenDetails tokenDetails)
  {

    var JwtSettings = this.configuration.GetSection("JwtSettings");

    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, tokenDetails.UserId),
      new Claim(JwtRegisteredClaimNames.Email, tokenDetails.Email),
      new Claim(ClaimTypes.Role, tokenDetails.Role),
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecretKey"]));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var tokem = new JwtSecurityToken(
      issuer: JwtSettings["Issuer"],
      audience: JwtSettings["Audience"],
      claims: claims,
      expires: DateTime.Now.AddMinutes(Convert.ToDouble(JwtSettings["ExpirationMinutes"])),
      signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(tokem);
  }
}