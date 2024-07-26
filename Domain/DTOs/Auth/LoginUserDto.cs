namespace Domain.DTOs.Auth;

/// <summary>
/// Representação da resposta do login de usuário
/// </summary>
public class LoginUserDto
{
	public ApplicationUserDto ApplicationUser { get; set; } = null!;

	public string Token { get; set; } = string.Empty;

	public DateTime TokenValidation { get; set; }
}
