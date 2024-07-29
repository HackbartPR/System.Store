using Domain.DTOs.Auth;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.Auth.Application.Services.AuthTokenGenerator;
using Service.Auth.Core.Entities;
using Service.Auth.Infrastructure;
using System.Net;

namespace Service.Auth.Application.Features.LoginUser;

/// <summary>
/// Responsável por realizar o login de um usuário
/// </summary>
public class LoginUserFeature : IRequestHandler<LoginUserCommand, BaseResponse<LoginUserDto?>>
{
	private readonly DbAuthContext context;
	private readonly UserManager<ApplicationUser> userManager;
	private readonly ITokenGenerator tokenGenerator;

	/// <summary>
	/// Construtor
	/// </summary>
	/// <param name="context"></param>
	/// <param name="userManager"></param>
	/// <param name="tokenGenerator"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public LoginUserFeature(DbAuthContext context, UserManager<ApplicationUser> userManager, ITokenGenerator tokenGenerator)
	{
		this.context = context ?? throw new ArgumentNullException(nameof(context));
		this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		this.tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
	}

	public async Task<BaseResponse<LoginUserDto?>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		ApplicationUser? user = await context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email!.Equals(request.UserName), cancellationToken);
		if (user == null)
			return new BaseResponse<LoginUserDto?>(null, false, "Usuário ou senha não encontrado.", HttpStatusCode.NotFound);

		bool result = await userManager.CheckPasswordAsync(user, request.Password);
		if (!result)
			return new BaseResponse<LoginUserDto?>(null, false, "Usuário ou senha não encontrado.", HttpStatusCode.NotFound);

		SecurityTokenDescriptor tokenDescriptor = await tokenGenerator.GetAccessToken(user);

		return new BaseResponse<LoginUserDto?>(new LoginUserDto()
		{
			ApplicationUser = new ApplicationUserDto()
			{
				Id = user.Id,
				Email = user.UserName!,
				Name = user.Name,
				PhoneNumber = user.PhoneNumber!
			},
			Token = tokenGenerator.HashToken(tokenDescriptor),
			TokenValidation = (DateTime) tokenDescriptor.Expires!
		}, true, "Usuário autenticado com sucesso.", HttpStatusCode.OK);
	}
}
