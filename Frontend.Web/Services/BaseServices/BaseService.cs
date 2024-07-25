using Domain.Enums;
using Domain.Requests.Base;
using Domain.Responses.Base;
using System.Text;
using System.Text.Json;

namespace Frontend.Web.Services.BaseServices;

/// <summary>
/// Classe base para realizar as requisições http para os microsserviços
/// </summary>
public class BaseService : IBaseService
{
    private readonly IHttpClientFactory httpClientFactory;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BaseService(IHttpClientFactory httpClientFactory)
        => this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

    /// <summary>
    /// Responsável por realizar a requisição
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResponse<TResponse?>> SendAsync<TRequest, TResponse>(BaseRequest<TRequest?> request, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpClient client = httpClientFactory.CreateClient();

            HttpRequestMessage message = new();
            //token

            message.RequestUri = new Uri(request.Url);
            message.Method = request.Method switch
            {
                ERequestMethod.POST => HttpMethod.Post,
                ERequestMethod.PUT => HttpMethod.Put,
                ERequestMethod.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };

            if (request.Data != null)
                message.Content = new StringContent(JsonSerializer.Serialize(request.Data), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(message, cancellationToken);
            BaseResponse<TResponse?>? baseResponse = new();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
            baseResponse = await JsonSerializer.DeserializeAsync<BaseResponse<TResponse?>>(contentStream, options, cancellationToken);

            if (baseResponse == null)
                return new BaseResponse<TResponse?>()
                {
                    Message = "Erro ao processar a solicitação, tente mais tarde novamente",
                    Success = false,
                    StatusCode = response.StatusCode,
                };

            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<TResponse?>()
            {
                Message = ex.Message,
                Success = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
            };
        }
    }

    /// <summary>
    /// Responsável por realizar a requisição paginada
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BasePagedResponse<TResponse?>> SendPagedAsync<TRequest, TResponse>(BaseRequest<TRequest?> request, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpClient client = httpClientFactory.CreateClient();

            HttpRequestMessage message = new();
            //token

            message.RequestUri = new Uri(request.Url);
            message.Method = request.Method switch
            {
                ERequestMethod.POST => HttpMethod.Post,
                ERequestMethod.PUT => HttpMethod.Put,
                ERequestMethod.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };

            if (request.Data != null)
                message.Content = new StringContent(JsonSerializer.Serialize(request.Data), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(message, cancellationToken);
            BasePagedResponse<TResponse?>? baseResponse = new();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
            baseResponse = await JsonSerializer.DeserializeAsync<BasePagedResponse<TResponse?>>(contentStream, options, cancellationToken);

            if (baseResponse == null)
                return new BasePagedResponse<TResponse?>()
                {
                    Message = "Erro ao processar a solicitação, tente mais tarde novamente",
                    Success = false,
                    StatusCode = response.StatusCode,
                    TotalRegisters = 0,
                    TotalPages = 0,
                };

            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BasePagedResponse<TResponse?>()
            {
                Message = ex.Message,
                Success = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
            };
        }
    }
}
