using Domain.DTOs.Auth;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Auth.Core.Entities;
using Service.Auth.Infrastructure;
using System.Net;

namespace Service.Auth.Application.Features.RegisterUser;

public class RegisterUserFeature : IRequestHandler<RegisterUserCommand, BaseResponse<ApplicationUserDto?>>
{
	private readonly DbAuthContext context;
	private readonly UserManager<ApplicationUser> userManager;

	public RegisterUserFeature(DbAuthContext context, UserManager<ApplicationUser> userManager)
	{
		this.context = context ?? throw new ArgumentNullException(nameof(context));
		this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
	}

	public async Task<BaseResponse<ApplicationUserDto?>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		if (await context.ApplicationUsers.AnyAsync(u => u.UserName!.Equals(request.Email) || u.Email!.Equals(request.Email), cancellationToken))
			return new BaseResponse<ApplicationUserDto?>(null, false, "Já existe um usuário com este 'Email' cadastrado.", HttpStatusCode.Conflict);

		ApplicationUser newUser = new()
		{
			UserName = request.Email,
			Email = request.Email,
			NormalizedEmail = request.Email.ToUpper(),
			NormalizedUserName = request.Email.ToUpper(),
			Name = request.Name,
			PhoneNumber = request.PhoneNumber,
		};

		IdentityResult? result = await userManager.CreateAsync(newUser, request.Password);
		if (result.Errors.Count() > 0)
			return BuildError(result.Errors);

		return new BaseResponse<ApplicationUserDto?>(new ApplicationUserDto()
		{
			Id = newUser.Id,
			Email = request.Email,
			Name = request.Name,
			PhoneNumber = request.PhoneNumber,
		}, true, "Usuário criado com sucesso.", HttpStatusCode.Created);
	}

	private BaseResponse<ApplicationUserDto?> BuildError(IEnumerable<IdentityError> errors)
	{
		return new BaseResponse<ApplicationUserDto?>()
		{
			Data = null,
			Success = false,
			StatusCode = HttpStatusCode.BadRequest,
			Errors = errors.Select(e => new ModelError(e.Description)).ToList()
		};
	}
}
