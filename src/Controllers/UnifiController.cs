using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using captive_portal_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace captive_portal_api.Controllers;

[ApiController]
[Route("[Controller]")]
public class UnifiController : ControllerBase
{


    private readonly ILogger<UnifiController> _logger;

    public UnifiController(ILogger<UnifiController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Login")]
    [Route("Login")]
    public async Task Login()
    {
        return;
    }
}
