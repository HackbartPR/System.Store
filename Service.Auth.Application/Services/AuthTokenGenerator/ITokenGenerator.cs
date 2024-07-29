using Microsoft.IdentityModel.Tokens;
using Service.Auth.Core.Entities;

namespace Service.Auth.Application.Services.AuthTokenGenerator;

/// <summary>
/// Responsável por criar o contrato para todos os geradores de token de autenticação
/// </summary>
public interface ITokenGenerator
{
	/// <summary>
	/// Responsável por criar o Descriptor do token
	/// </summary>
	/// <param name="user"></param>
	/// <returns></returns>
	Task<SecurityTokenDescriptor> GetAccessToken(ApplicationUser user);

	/// <summary>
	/// Responsável por criar o token
	/// </summary>
	/// <param name="tokenDescriptor"></param>
	/// <returns></returns>
	string HashToken(SecurityTokenDescriptor tokenDescriptor);
}
