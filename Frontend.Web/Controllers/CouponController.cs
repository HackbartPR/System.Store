using Domain.DTOs;
using Domain.Requests.CouponService;
using Domain.Responses.Base;
using Frontend.Web.Services.CouponServices;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Web.Controllers;

/// <summary>
/// Controller para manipulação de cupons promocionais
/// </summary>
public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="couponService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CouponController(ICouponService couponService)
        => _couponService = couponService ?? throw new ArgumentNullException(nameof(couponService));

    /// <summary>
    /// Lista todos os cupons
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> CouponIndex(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        GetCouponRequest request = new()
        {
            Page = pageNumber,
            PageSize = pageSize,
        };

        BasePagedResponse<CouponDto?> couponsPaged = await _couponService.GetCouponAsync(request ,cancellationToken);
		ViewBag.PageNumber = pageNumber;
		ViewBag.PageSize = pageSize;
		return View(couponsPaged);
    }

    /// <summary>
    /// Cria um novo cupom
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult CouponCreate()
    {
        return View();
	}

    [HttpPost]
    public async Task<IActionResult> CouponCreate(PostCouponRequest request, CancellationToken cancellation = default) 
    {
        BaseResponse<CouponDto?> response = await _couponService.CreateCouponAsync(request ,cancellation);
        if (response.Success)
            return RedirectToAction(nameof(CouponIndex));

        return View(request);
    }
}
