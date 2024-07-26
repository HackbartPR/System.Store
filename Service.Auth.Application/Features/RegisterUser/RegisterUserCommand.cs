using Domain.DTOs.Auth;
using Domain.Responses;
using MediatR;

namespace Service.Auth.Application.Features.RegisterUser;

public class RegisterUserCommand : IRequest<BaseResponse<ApplicationUserDto?>>
{
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
}
