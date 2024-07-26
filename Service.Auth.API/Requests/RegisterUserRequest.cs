using System.ComponentModel.DataAnnotations;

namespace Service.Auth.API.Requests;

/// <summary>
/// Representação da requisição necessária para realizar um cadastro no sistema
/// </summary>
public class RegisterUserRequest
{
	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Nome' não pode ser nulo ou vazio.")]
	[MinLength(3)]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "O campo 'Email' não pode ser nulo")]
	[EmailAddress(ErrorMessage = "O campo 'Email' não ´deve estar em um formato válido")]
	public string Email { get; set; } = string.Empty;

	[Required(ErrorMessage = "O campo 'Senha' não pode ser nulo")]
	public string Password { get; set; } = string.Empty;

	[Required(ErrorMessage = "O campo 'Telefone' não pode ser nulo")]
	public string PhoneNumber { get; set; } = string.Empty;
}
