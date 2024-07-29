using Domain.Responses;
using MediatR;

namespace Service.Product.Application.Features.Delete;

public class DeleteProductCommand : IRequest<BaseResponse<bool>>
{
    /// <summary>
    /// Identificação
    /// </summary>
    public int Id { get; set; }
}
