using Domain.Responses;
using MediatR;

namespace Service.Coupon.Application.Features.Delete;

public class DeleteCouponCommand : IRequest<BaseResponse<bool>>
{
    /// <summary>
    /// Identificação
    /// </summary>
    public int Id { get; set; }
}
