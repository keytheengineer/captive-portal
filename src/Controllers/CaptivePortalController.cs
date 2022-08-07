using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using captive_portal_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace captive_portal_api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CaptivePortalController : ControllerBase
{
    private readonly ILogger<CaptivePortalController> _logger;
    private readonly CaptivePortalService _service;

    public CaptivePortalController(ILogger<CaptivePortalController> logger, CaptivePortalService service)
    {
        _logger = logger;
        _service = service;
    }

           
    [HttpGet]
    [Route("RequestGuestAccess")]
    public async Task<ActionResult<bool>> RequestGuestAccess()
    {
        string ip = string.Empty;
        IPAddress? remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
        if (remoteIpAddress != null)
        {
            if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                _logger.LogInformation("IPV6");
                remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork);
            }
            ip = remoteIpAddress.ToString();
        }
        //! TESTING ONLY
        ip = "";
        var result = Task.Run(() => _service.RequestGuestAccess(ip));
        return Ok(await result);
    }

    [HttpGet]
    [Route("TestClientIp")]
    public ActionResult<string> GetClientIp()
    {
        string ip = string.Empty;
        IPAddress? remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
        if (remoteIpAddress != null)
        {
            if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                _logger.LogInformation("IPV6");
                remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork);
            }
            ip = remoteIpAddress.ToString();
            return Ok(ip);
        }
        return NoContent();
    }


}
