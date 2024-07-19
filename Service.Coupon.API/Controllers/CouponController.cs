using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Coupon.API.CrossCutting.BaseController;
using Service.Coupon.Application.DTOs;
using Service.Coupon.Application.Features.Get.Request;
using Service.Coupon.Infrastructure.CrossCutting.BaseResponses;

namespace Service.Coupon.API.Controllers;

/// <summary>
/// Controller
/// </summary>
[Route("api/v1/coupon")]
public class CouponController : BaseController
{
    private readonly IMediator mediator;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="logger"></param>
    public CouponController(ILogger<CouponController> logger, IMediator mediator) : base(logger)
        => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCouponRequest request, CancellationToken cancellationToken)
    {
        BasePagedResponse<CouponDto> response = new();

        try
        {
            response = await mediator.Send(request, cancellationToken);
            response.Success = true;
            response.Message = "Consulta realizada com sucesso";
        }
        catch (Exception ex)
        {
            response.BuildError(_logger, ex);
        }

        return Result(response);
    }
}
