using Domain.DTOs.Auth;
using Domain.Responses;
using MediatR;

namespace Service.Auth.Application.Features.LoginUser;

public class LoginUserCommand : IRequest<BaseResponse<LoginUserDto?>>
{
	public string UserName { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}
