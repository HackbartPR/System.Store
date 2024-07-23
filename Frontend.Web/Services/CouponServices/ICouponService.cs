using Domain.DTOs;
using Domain.Requests.CouponService;
using Domain.Responses.Base;

namespace Frontend.Web.Services.CouponServices;

public interface ICouponService
{
    /// <summary>
    /// Realiza a busca de todos os cupons de acordo com o filtro fornecido
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<BasePagedResponse<CouponDto?>> GetCouponAsync(GetCouponRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Realiza a criação de um coupon
    /// </summary>
    /// <param name="coupon"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<BaseResponse<CouponDto?>> CreateCouponAsync(PostCouponRequest coupon, CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza um cupom promocional
    /// </summary>
    /// <param name="id"></param>
    /// <param name="coupon"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<BaseResponse<CouponDto?>> UpdateCouponAsync(int id, PutCouponRequest coupon, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove um cupom promocional
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<BaseResponse<bool>> DeleteCouponAsync(string id, CancellationToken cancellationToken = default);
}
