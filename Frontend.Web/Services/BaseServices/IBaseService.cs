using Domain.Requests.Base;
using Domain.Responses.Base;

namespace Frontend.Web.Services.BaseServices;

/// <summary>
/// Classe de contrato para todas as implementações do base service
/// </summary>
public interface IBaseService
{
    /// <summary>
    /// Responsável por realizar a requisição
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<BaseResponse<TResponse?>> SendAsync<TRequest, TResponse>(BaseRequest<TRequest?> request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Responsável por realizar a requisição paginada
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<BasePagedResponse<TResponse?>> SendPagedAsync<TRequest, TResponse>(BaseRequest<TRequest?> request, CancellationToken cancellationToken = default);
}
