using Domain.Responses;
using MediatR;

namespace Service.Auth.Application.Features.AssignRole;

/// <summary>
/// Representação da requisição necessária para criar um role
/// </summary>
public class AssignRoleCommand : IRequest<BaseResponse<bool>>
{
	public string Email { get; set; } = string.Empty;

	public string RoleName { get; set; } = string.Empty;
}
