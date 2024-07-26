using Domain.DTOs.Auth;
using Domain.Responses;
using MediatR;

namespace Service.Auth.Application.Features.LoginUser;

/// <summary>
/// Responsável por realizar o login de um usuário
/// </summary>
public class LoginUserFeature : IRequestHandler<LoginUserCommand, BaseResponse<LoginUserDto?>>
{
	public Task<BaseResponse<LoginUserDto?>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
