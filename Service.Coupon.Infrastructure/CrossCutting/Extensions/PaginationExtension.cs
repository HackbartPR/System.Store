using Service.Coupon.Infrastructure.CrossCutting.BaseResponses;
using Microsoft.EntityFrameworkCore;
using Service.Coupon.Infrastructure.CrossCutting.BaseRequests;
using System.Linq.Expressions;
using System.Reflection;

namespace Service.Coupon.Infrastructure.CrossCutting.Extensions;

/// <summary>
/// Responsável por realizar a consulta paginada de uma entidade
/// </summary>
public static class PaginationExtension
{
    /// <summary>
    /// Realiza a consulta paginada
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="query"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<BasePagedResponse<TResponse>> Paginate<TResponse>(this IQueryable<TResponse> query, BasePagedRequest request, CancellationToken cancellationToken) where TResponse : class
    {
        if (request.OrderByProperty.StartsWith('-'))
            query = query.OrderByPropertyDescending(request.OrderByProperty[1..]);
        else
            query = query.OrderByProperty(request.OrderByProperty);

        BasePagedResponse<TResponse> response = new();
        response.TotalRegisters = await query.CountAsync(cancellationToken);
        response.TotalPages = (int)Math.Round((decimal) response.TotalRegisters / request.PageSize, mode: MidpointRounding.ToPositiveInfinity);
        response.Data = await query
            .Skip(request.PageSize * (request.Page - 1))
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return response;
    }

    /// <summary>
    /// Orderna um resultado de forma ascendente pelo nome de uma propriedade da entidade
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="query"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    private static IQueryable<TData> OrderByProperty<TData>(this IQueryable<TData> query, string propertyName)
    {
        if (typeof(TData).GetProperty(propertyName, BindingFlags.IgnoreCase |
            BindingFlags.Public | BindingFlags.Instance) == null)
            return query;

        ParameterExpression parameterExpression = Expression.Parameter(typeof(TData));
        Expression orderByProperty = Expression.Property(parameterExpression, propertyName);
        LambdaExpression lambda = Expression.Lambda(orderByProperty, parameterExpression);
        MethodInfo genericMethod = OrderByMethod.MakeGenericMethod(typeof(TData), orderByProperty.Type);
        object ret = genericMethod.Invoke(null, new object[] { query, lambda })!;
        return (IQueryable<TData>)ret!;
    }

    /// <summary>
    /// Orderna um resultado de forma descendente pelo nome de uma propriedade da entidade
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="query"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    private static IQueryable<TData> OrderByPropertyDescending<TData>(this IQueryable<TData> query, string propertyName)
    {
        if (typeof(TData).GetProperty(propertyName, BindingFlags.IgnoreCase |
            BindingFlags.Public | BindingFlags.Instance) == null)
            return query;

        ParameterExpression parameterExpression = Expression.Parameter(typeof(TData));
        Expression orderByProperty = Expression.Property(parameterExpression, propertyName);
        LambdaExpression lambda = Expression.Lambda(orderByProperty, parameterExpression);
        MethodInfo genericMethod = OrderByDescendingMethod.MakeGenericMethod(typeof(TData), orderByProperty.Type);
        object ret = genericMethod.Invoke(null, new object[] { query, lambda })!;
        return (IQueryable<TData>)ret!;
    }

    /// <summary>
    /// Recupera o método Queryble.OrderBy
    /// </summary>
    private static readonly MethodInfo OrderByMethod =
    typeof(Queryable).GetMethods()
        .Single(method => method.Name == "OrderBy" && method.GetParameters().Length == 2);

    /// <summary>
    /// Recupera o método Queryble.OrderByDescending
    /// </summary>
    private static readonly MethodInfo OrderByDescendingMethod =
        typeof(Queryable).GetMethods()
            .Single(method => method.Name == "OrderByDescending" && method.GetParameters().Length == 2);
}
