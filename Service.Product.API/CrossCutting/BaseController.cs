using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Service.Product.API.CrossCutting;

/// <summary>
/// Controller base para servir todos os outros controller
/// </summary>
[ApiController]
[Produces("application/json")]
public class BaseController : Controller
{
    protected readonly ILogger logger;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BaseController(ILogger logger)
        => this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

    /// <summary>
    /// Transforma o <seealso cref="BaseResponse"/> em um IActionResult
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected IActionResult Result(BaseResponse response)
    {
        response.RequestId = HttpContext.TraceIdentifier;
        logger.LogInformation("ResponseBody: {Body}", response);

        if (response.StatusCode.HasValue)
            return StatusCode((int)response.StatusCode, response);

        if (response.Success)
            return Ok(response);

        return StatusCode(500, response);
    }

    /// <summary>
    /// Rotina é executada antes da requisição ficar disponível para uso no controller
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (!context.ModelState.IsValid)
        {
            BaseResponse response = new();
            response.RequestId = HttpContext.TraceIdentifier;
            response.Success = false;
            response.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => new ModelError(e.ErrorMessage)).ToList();
            response.StatusCode = HttpStatusCode.BadRequest;

            context.Result = Result(response);
        }

        base.OnActionExecuted(context);
    }
}
