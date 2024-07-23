using Domain.DTOs;
using Domain.Enums;
using Domain.Requests.Base;
using Domain.Requests.CouponService;
using Domain.Responses.Base;
using Frontend.Web.CrossCutting.Settings;
using Frontend.Web.Helpers;
using Frontend.Web.Services.BaseServices;
using Microsoft.Extensions.Options;

namespace Frontend.Web.Services.CouponServices;

public class CouponService : ICouponService
{
    private readonly IBaseService baseService;
    private readonly ServicesUrl servicesUrl;

    public CouponService(IBaseService baseService, IOptions<ServicesUrl> options)
    {
        this.baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
        servicesUrl = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Realiza a criação de um coupon
    /// </summary>
    /// <param name="coupon"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResponse<CouponDto?>> CreateCouponAsync(PostCouponRequest coupon, CancellationToken cancellationToken = default)
    {
        return await baseService.SendAsync<PostCouponRequest?, CouponDto?>(new BaseRequest<PostCouponRequest?>
        {
            Method = ERequestMethod.POST,
            Url = servicesUrl.CouponAPI,
            AccessToken = "",
            Data = coupon,
        }, cancellationToken);
    }

    /// <summary>
    /// Remove um cupom promocional
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResponse<bool>> DeleteCouponAsync(string id, CancellationToken cancellationToken = default)
    {
        return await baseService.SendAsync<CouponDto?, bool>(new BaseRequest<CouponDto?>
        {
            Method = ERequestMethod.DELETE,
            Url = $"{servicesUrl.CouponAPI}/{id}",
            AccessToken = "",
        }, cancellationToken);
    }

    /// <summary>
    /// Realiza a busca de todos os cupons de acordo com o filtro fornecido
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BasePagedResponse<CouponDto?>> GetCouponAsync(GetCouponRequest request, CancellationToken cancellationToken = default)
    {
        return await baseService.SendPagedAsync<CouponDto?, CouponDto?>(new BaseRequest<CouponDto?>()
        {
            Method = ERequestMethod.GET,
            Url = ServicesHelper.CreateUrlWithQuery(servicesUrl.CouponAPI, request),
            AccessToken = "",
        }, cancellationToken);
    }

    /// <summary>
    /// Atualiza um cupom promocional
    /// </summary>
    /// <param name="id"></param>
    /// <param name="coupon"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResponse<CouponDto?>> UpdateCouponAsync(int id, PutCouponRequest coupon, CancellationToken cancellationToken = default)
    {
        return await baseService.SendAsync<PutCouponRequest?, CouponDto?>(new BaseRequest<PutCouponRequest?>
        {
            Method = ERequestMethod.PUT,
            Url = $"{servicesUrl.CouponAPI}/{id}",
            AccessToken = "",
            Data = coupon,
        }, cancellationToken);
    }
}
