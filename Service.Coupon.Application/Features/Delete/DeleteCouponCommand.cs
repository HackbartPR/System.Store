using MediatR;
using Service.Coupon.Infrastructure.CrossCutting.BaseResponses;

namespace Service.Coupon.Application.Features.Delete;

public class DeleteCouponCommand : IRequest<BaseResponse<bool>>
{
    /// <summary>
    /// Identificação
    /// </summary>
    public int Id { get; set; }
}
