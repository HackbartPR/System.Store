using Domain.DTOs.Coupon;
using Domain.Requests;
using Domain.Responses;
using MediatR;

namespace Service.Product.Application.Features.Get;

/// <summary>
/// Representação da requisição para listar os produtos
/// </summary>
public class GetProductCommand : BasePagedRequest, IRequest<BasePagedResponse<ProductDto>>
{
    /// <summary>
    /// Identificação
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Nome
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Nome da categoria
    /// </summary>
	public string? CategoryName { get; set; }
}
