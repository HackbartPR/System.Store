using Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Auth.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Auth.Application.Services.AuthTokenGenerator;

/// <summary>
/// Responsável por gerar um token de autenticação
/// </summary>
public class JwtTokenGenerator : ITokenGenerator
{
	private readonly AuthenticationSettings settings;

	/// <summary>
	/// Construtor
	/// </summary>
	/// <param name="options"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public JwtTokenGenerator(IOptions<AuthenticationSettings> options)
		=>this.settings = options.Value ?? throw new ArgumentNullException(nameof(options));

	/// <summary>
	/// Responsável por criar o Descriptor do token
	/// </summary>
	/// <param name="user"></param>
	/// <returns></returns>
	public SecurityTokenDescriptor GetAccessToken(ApplicationUser user)
	{
		List<Claim> claims = new()
		{
			new Claim(JwtRegisteredClaimNames.Name, user.Name),
			new Claim(JwtRegisteredClaimNames.Email, user.Email!)
		};

		byte[] key = Encoding.ASCII.GetBytes(settings.SecretKey);

		return new SecurityTokenDescriptor()
		{
			Subject = new ClaimsIdentity(claims),
			Audience = settings.Audience,
			Issuer = settings.Issuer,
			Expires = DateTime.UtcNow.AddHours(double.TryParse(settings.ExpireAtInHours, out double hour) ? hour : 24),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
		};
	}

	/// <summary>
	/// Responsável por criar o token
	/// </summary>
	/// <param name="user"></param>
	/// <returns></returns>
	public string HashToken(SecurityTokenDescriptor tokenDescriptor)
	{
		JwtSecurityTokenHandler tokenHandler = new();
		SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}
