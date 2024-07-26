namespace Domain.DTOs.Auth;

/// <summary>
/// Representação de um usuário do sistema
/// </summary>
public class ApplicationUserDto
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}
