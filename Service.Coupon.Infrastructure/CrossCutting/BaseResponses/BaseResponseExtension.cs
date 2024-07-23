using Domain.Responses.Base;
using Microsoft.Extensions.Logging;
using Service.Coupon.Infrastructure.CrossCutting.Exceptions;
using System.Runtime.CompilerServices;

namespace Service.Coupon.Infrastructure.CrossCutting.BaseResponses;

/// <summary>
/// Extensão para a classe <seealso cref="BaseResponse"/> 
/// </summary>
public static class BaseResponseExtension
{
    /// <summary>
    /// Constroi uma responsta de erro.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="logger"></param>
    /// <param name="ex"></param>
    /// <param name="genericMessage"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void BuildError(this BaseResponse response, ILogger logger, Exception ex, string genericMessage = "Ocorreu um erro inesperado ao processar a sua solicitação. Verifique os dados e tente novamente.")
    {
        response.Success = false;

        if (ex is CouponException)
        {
            CouponException couponException = ex as CouponException ?? new();
            response.StatusCode = couponException.StatusCode;
            response.Message = couponException.Message;
            response.Error = couponException.InnerException?.Message ?? string.Empty;
            response.Version = "1.0";
        }
        else
        {
            response.Message = genericMessage;
            response.Error = ex.Message;
        }

        logger.LogError(ex, response.Message);
    }
}
