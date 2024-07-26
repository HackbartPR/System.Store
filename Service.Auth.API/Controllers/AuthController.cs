using Domain.DTOs.Auth;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Auth.API.CrossCutting;
using Service.Auth.API.Requests;
using Service.Auth.Application.Features.LoginUser;
using Service.Auth.Application.Features.RegisterUser;

namespace Service.Auth.API.Controllers;

/// <summary>
/// Controller
/// </summary>
[Route("api/v1/auth")]
public class AuthController : BaseController
{
	private readonly IMediator mediator;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="mediator"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public AuthController(ILogger<AuthController> logger, IMediator mediator) : base(logger)
		=> this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

	/// <summary>
	/// Registrar um novo usuário
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	[HttpPost("register")]
	public async Task<IActionResult> Register (RegisterUserRequest request, CancellationToken cancellationToken)
	{
		BaseResponse<ApplicationUserDto?> response = new();

		try
		{
			RegisterUserCommand command = new()
			{
				Email = request.Email,
				Name = request.Name,
				Password = request.Password,
				PhoneNumber = request.PhoneNumber
			};

			response = await mediator.Send(command, cancellationToken);
		}
		catch (Exception ex)
		{
			response.BuildError(logger, ex);
		}

		return Result(response);
	}

	/// <summary>
	/// Realiza a login de um usuário
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
	{
		BaseResponse<LoginUserDto?> response = new();

		try
		{
			LoginUserCommand command = new()
			{
				UserName = request.UserName,
				Password = request.Password,
			};

			response = await mediator.Send(command, cancellationToken);
		}
		catch (Exception ex)
		{
			response.BuildError(logger, ex);
		}

		return Result(response);
	}
}
