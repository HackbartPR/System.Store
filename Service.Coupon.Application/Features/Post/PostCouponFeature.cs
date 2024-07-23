using Domain.DTOs;
using Domain.Responses.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Coupon.Core.Entities;
using Service.Coupon.Infrastructure.Database;
using System.Net;

namespace Service.Coupon.Application.Features.Post;

public class PostCouponFeature : IRequestHandler<PostCouponCommand, BaseResponse<CouponDto?>>
{
    private readonly DbCouponContext dbContext;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="dbContext"></param>
    public PostCouponFeature(DbCouponContext dbContext)
        => this.dbContext = dbContext;

    /// <summary>
    /// Cadastra um novo cupon promociona
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResponse<CouponDto?>> Handle(PostCouponCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Coupons.AnyAsync(c => c.CouponCode.Equals(request.CouponCode), cancellationToken))
            return new BaseResponse<CouponDto?>(null, false, "Código informado já está em uso", HttpStatusCode.Conflict);

        CouponEntity coupon = new()
        {
            CouponCode = request.CouponCode,
            MinAmount = (int)request.MinAmount!,
            DiscountAmount = request.DiscountAmount,
            CreatedAt = DateTime.UtcNow,
        };

        await dbContext.Coupons.AddAsync(coupon, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new BaseResponse<CouponDto?>(new CouponDto
        {
            Id = coupon.Id,
            MinAmount = coupon.MinAmount,
            CouponCode = coupon.CouponCode,
            DiscountAmount = coupon.DiscountAmount,
        }, true, "Cupon cadastrado com sucesso", HttpStatusCode.Created);
    }
}
