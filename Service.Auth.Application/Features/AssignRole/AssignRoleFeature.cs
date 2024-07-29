using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Auth.Core.Entities;
using Service.Auth.Infrastructure;
using System.Net;

namespace Service.Auth.Application.Features.AssignRole;

/// <summary>
/// Responsável por cadastrar um usuário em um Role existente
/// </summary>
public class AssignRoleFeature : IRequestHandler<AssignRoleCommand, BaseResponse<bool>>
{
	private readonly DbAuthContext context;
	private readonly UserManager<ApplicationUser> userManager;
	private readonly RoleManager<IdentityRole> roleManager;

	/// <summary>
	/// Construtor
	/// </summary>
	/// <param name="context"></param>
	/// <param name="roleManager"></param>
	/// <param name="userManager"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public AssignRoleFeature(RoleManager<IdentityRole> roleManager, DbAuthContext context, UserManager<ApplicationUser> userManager)
	{
		this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
		this.context = context ?? throw new ArgumentNullException(nameof(context));
		this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
	}

	public async Task<BaseResponse<bool>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
	{
		ApplicationUser? user = await context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email!.Equals(request.Email), cancellationToken);
		if (user == null)
			return new BaseResponse<bool>(false, false, "Usuário não encontrado.", HttpStatusCode.NotFound);

		if (!await roleManager.RoleExistsAsync(request.RoleName))
			return new BaseResponse<bool>(false, false, "Role não encontrado.", HttpStatusCode.NotFound);

		await userManager.AddToRoleAsync(user, request.RoleName);

		return new BaseResponse<bool>(true, true, "Role adicionado ao usuário com sucesso.", HttpStatusCode.OK);
	}
}
