using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace captive_portal_api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CaptivePortalController : ControllerBase
{
    private readonly ILogger<CaptivePortalController> _logger;

    public CaptivePortalController(ILogger<CaptivePortalController> logger)
    {
        _logger = logger;
    }
}
