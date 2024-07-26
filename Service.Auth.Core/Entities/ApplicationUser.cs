using Microsoft.AspNetCore.Identity;

namespace Service.Auth.Core.Entities;

/// <summary>
/// Customização da classe IdentityUser
/// </summary>
public class ApplicationUser : IdentityUser
{
	public string Name { get; set; } = string.Empty;
}
