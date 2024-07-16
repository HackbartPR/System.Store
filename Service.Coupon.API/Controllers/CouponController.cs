using Microsoft.AspNetCore.Mvc;
using Service.Coupon.API.CrossCutting.BaseController;
using Service.Coupon.Application.DTOs;
using Service.Coupon.Application.Features.Get.Request;
using Service.Coupon.Infrastructure.CrossCutting.BaseResponses;

namespace Service.Coupon.API.Controllers;

/// <summary>
/// Controller
/// </summary>
public class CouponController : BaseController
{
    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="logger"></param>
    public CouponController(ILogger logger) : base(logger) {}

    public async Task<IActionResult> Get(GetCouponRequest request, CancellationToken cancellationToken)
    {
        BasePagedResponse<CouponDto> response = new();

        try
        {
        }
        catch (Exception ex)
        {

        }
    }
}
