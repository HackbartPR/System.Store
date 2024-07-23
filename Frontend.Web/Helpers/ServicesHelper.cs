using Domain.Requests.Base;
using System.Web;

namespace Frontend.Web.Helpers;

/// <summary>
/// Classe responsável por métodos auxiliares na rotina dos services
/// </summary>
public static class ServicesHelper
{
    /// <summary>
    /// Cria uma URL com query à partir de uma classe
    /// </summary>
    /// <param name="baseUrl"></param>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    public static string CreateUrlWithQuery(string baseUrl, BasePagedRequest queryParams)
    {
        var properties = from p in queryParams.GetType().GetProperties()
                         where p.GetValue(queryParams, null) != null
                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(queryParams)?.ToString() ?? string.Empty);

        var queryString = String.Join("&", properties.ToArray());

        return $"{baseUrl}?{queryString}";
    }
}
