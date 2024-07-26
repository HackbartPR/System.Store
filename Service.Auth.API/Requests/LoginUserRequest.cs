using System.ComponentModel.DataAnnotations;

namespace Service.Auth.API.Requests;

/// <summary>
/// Representação do login de um usuário
/// </summary>
public class LoginUserRequest
{
	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Nome do Usuário' não pode ser nulo ou vazio.")]
	public string UserName { get; set;} = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Senha' não pode ser nulo ou vazio.")]
	public string Password { get; set;} = string.Empty;
}
