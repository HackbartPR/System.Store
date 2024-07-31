using Domain.Settings;
using Microsoft.AspNetCore.Identity;
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
	private readonly UserManager<ApplicationUser> userManager;

	/// <summary>
	/// Construtor
	/// </summary>
	/// <param name="options"></param>
	/// <param name="userManager"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public JwtTokenGenerator(IOptions<AuthenticationSettings> options, UserManager<ApplicationUser> userManager)
	{
		this.settings = options.Value ?? throw new ArgumentNullException(nameof(options));
		this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
	}

	/// <summary>
	/// Responsável por criar o Descriptor do token
	/// </summary>
	/// <param name="user"></param>
	/// <returns></returns>
	public async Task<SecurityTokenDescriptor> GetAccessToken(ApplicationUser user)
	{
		var userRoles = await userManager.GetRolesAsync(user);

		List<Claim> claims = new()
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id),
			new Claim(JwtRegisteredClaimNames.Name, user.Name),
			new Claim(JwtRegisteredClaimNames.Email, user.Email!),
		};

		claims.AddRange(userRoles.Select(r => new Claim(ClaimTypes.Role, r)));

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
