using Domain.DTOs;
using Domain.Requests.CouponService;
using Domain.Responses.Base;
using Frontend.Web.Services.CouponServices;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService ?? throw new ArgumentNullException(nameof(couponService));
        }

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
    }
}
