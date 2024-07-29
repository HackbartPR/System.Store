using Domain.Constants;

namespace Service.Auth.Core.Seeds;

/// <summary>
/// Seeds para os roles
/// </summary>
public static class RoleSeeds
{
	public static ICollection<string> Seeds()
	{
		return [ApplicationRoles.Admin, ApplicationRoles.Customer];
	}
}
