using System.ComponentModel.DataAnnotations;

namespace Service.Auth.API.Requests;

/// <summary>
/// Representação da requisição para vincular um usuário a um role específico
/// </summary>
public class AssignRoleRequest
{
	[Required(ErrorMessage = "O campo 'Email' não pode ser nulo")]
	[EmailAddress(ErrorMessage = "O campo 'Email' não ´deve estar em um formato válido")]
	public string Email { get; set; } = string.Empty;

	[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Nome do Role' não pode ser nulo ou vazio")]
	public string RoleName { get; set; } = string.Empty;
}
