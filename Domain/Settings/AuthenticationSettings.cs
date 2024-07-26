namespace Domain.Settings;

/// <summary>
/// Representação da configuração do token de autenticação passado no AppSettings
/// </summary>
public class AuthenticationSettings
{
	public const string Identifier = "Authentication";

	public string SecretKey { get; set; } = string.Empty;
	public string Issuer { get; set; } = string.Empty;
	public string Audience { get; set; } = string.Empty;
	public string ExpireAtInHours { get; set; } = string.Empty;
}
