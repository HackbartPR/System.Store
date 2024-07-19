using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Coupon.API.Controllers.Requests;
using Service.Coupon.API.CrossCutting.BaseController;
using Service.Coupon.Application.DTOs;
using Service.Coupon.Application.Features.Delete;
using Service.Coupon.Application.Features.Get;
using Service.Coupon.Application.Features.Post;
using Service.Coupon.Application.Features.Put;
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

    /// <summary>
    /// Retorna uma lista de coupons de acordo com o filtro passado
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Lista de Coupons</returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCouponRequest request, CancellationToken cancellationToken)
    {
        BasePagedResponse<CouponDto> response = new();

        try
        {
            GetCouponCommand command = new()
            {
                Id = request.Id,
                CouponCode = request.CouponCode,
                OrderByProperty = request.OrderByProperty,
                Page = request.Page,
                PageSize = request.PageSize
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
    /// Cadastra um novo cupom promocional
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostCouponRequest request, CancellationToken cancellationToken)
    {
        BaseResponse<CouponDto?> response = new();

        try
        {
            PostCouponCommand command = new()
            {
                MinAmount = request.MinAmount,
                CouponCode = request.CouponCode,
                DiscountAmount = request.DiscountAmount,
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
    /// Atualiza um cupom promocional
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PutCouponRequest request, CancellationToken cancellationToken)
    {
        BaseResponse<CouponDto?> response = new();

        try
        {
            PutCouponCommand command = new()
            {
                Id = id,
                MinAmount = request.MinAmount,
                CouponCode = request.CouponCode,
                DiscountAmount = request.DiscountAmount,
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
    /// remove um cupom promocional
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        BaseResponse<bool> response = new();

        try
        {
            DeleteCouponCommand command = new(){ Id = id };
            response = await mediator.Send(command, cancellationToken);
        }
        catch (Exception ex)
        {
            response.BuildError(logger, ex);
        }

        return Result(response);
    }
}
