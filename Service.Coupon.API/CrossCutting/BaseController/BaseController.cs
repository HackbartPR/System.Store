using Microsoft.AspNetCore.Mvc;
using Service.Coupon.Infrastructure.CrossCutting.BaseResponses;

namespace Service.Coupon.API.CrossCutting.BaseController;

/// <summary>
/// Controller base para servir todos os outros controller
/// </summary>
[ApiController]
[Produces("application/json")]
public class BaseController : Controller
{
    protected readonly ILogger _logger;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BaseController(ILogger logger)
        => _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    /// <summary>
    /// Transforma o <seealso cref="BaseResponse"/> em um IActionResult
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected IActionResult Result(BaseResponse response)
    {
        response.RequestId = HttpContext.TraceIdentifier;
        _logger.LogInformation("ResponseBody: {Body}", response);

        if (response.StatusCode.HasValue)
            return StatusCode((int)response.StatusCode, response);

        if (response.Success)
            return Ok(response);

        return StatusCode(500, response);
    }
}
