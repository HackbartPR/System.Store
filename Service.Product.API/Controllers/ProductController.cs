using Domain.DTOs.Coupon;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Product.API.CrossCutting;
using Service.Product.API.Requests;
using Service.Product.Application.Features.Delete;
using Service.Product.Application.Features.Get;
using Service.Product.Application.Features.Post;
using Service.Product.Application.Features.Put;

namespace Service.Product.API.Controllers;

/// <summary>
/// Controller
/// </summary>
[Route("api/v1/product")]
public class ProductController : BaseController
{
    private readonly IMediator mediator;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="logger"></param>
    public ProductController(ILogger<ProductController> logger, IMediator mediator) : base(logger)
        => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Retorna uma lista de produtos de acordo com o filtro passado
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Lista de Produtos</returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductRequest request, CancellationToken cancellationToken)
    {
        BasePagedResponse<ProductDto> response = new();

        try
        {
            GetProductCommand command = new()
            {
                Id = request.Id,
                Name = request.Name,
                CategoryName = request.CategoryName,
                OrderByProperty = request.OrderByProperty,
                Page = request.Page,
                PageSize = request.PageSize
            };

            response = await mediator.Send(command, cancellationToken);
        }
        catch (Exception ex)
        {
            response.BuildError(logger, ex);
        }

        return Result(response);
    }

    /// <summary>
    /// Cadastra um novo product
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostProductRequest request, CancellationToken cancellationToken)
    {
        BaseResponse<ProductDto?> response = new();

        try
        {
            PostProductCommand command = new()
            {
                Name = request.Name,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                Description = request.Description,
                CategoryName = request.CategoryName,
            };

            response = await mediator.Send(command, cancellationToken);
        }
        catch (Exception ex)
        {
            response.BuildError(logger, ex);
        }

        return Result(response);
    }

    /// <summary>
    /// Atualiza um product 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PutProductRequest request, CancellationToken cancellationToken)
    {
        BaseResponse<ProductDto?> response = new();

        try
        {
            PutProductCommand command = new()
            {
                Id = id,
                Name = request.Name,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                Description = request.Description,
                CategoryName = request.CategoryName,
            };

            response = await mediator.Send(command, cancellationToken);
        }
        catch (Exception ex)
        {
            response.BuildError(logger, ex);
        }

        return Result(response);
    }

    /// <summary>
    /// remove um product 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        BaseResponse<bool> response = new();

        try
        {
            DeleteProductCommand command = new(){ Id = id };
            response = await mediator.Send(command, cancellationToken);
        }
        catch (Exception ex)
        {
            response.BuildError(logger, ex);
        }

        return Result(response);
    }
}
