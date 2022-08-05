using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using captive_portal_api.Models;
using captive_portal_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace captive_portal_api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class UnifiController : ControllerBase
{
    private readonly ILogger<UnifiController> _logger;
    private readonly UnifiApiService _service;

    public UnifiController(ILogger<UnifiController> logger, UnifiApiService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Route("Login")]
    public async Task<AuthenticationResult> Login()
    {
        try
        {
            return await _service.Login();
        }
        catch (Exception e)
        {
            throw(e);
        }
    }
        
    [HttpGet]
    [Route("Logout")]
    public async Task<ActionResult> Logout()
    {
        try
        {
            var result = await _service.Logout();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }    
    
    [HttpGet]
    [Route("GetActiveClients")]
    public async Task<List<Client>> GetActiveClients()
    {
        try
        {
            return await _service.GetActiveClients();
        }
        catch (Exception e)
        {
            throw(e);
        }
    }

    [HttpGet]
    [Route("GetAllClients")]
    public async Task<List<Client>> GetAllClients()
    {
        try
        {
            return await _service.GetAllClients();
        }
        catch (Exception e)
        {
            throw(e);
        }
    }

    [HttpGet]
    [Route("GetDevices")]
    public async Task<List<Device>> GetDevices()
    {
        try
        {
            return await _service.GetDevices();
        }
        catch (Exception e)
        {
            throw(e);
        }
    }
        
    [HttpGet]
    [Route("GetSites")]
    public async Task<List<Site>> GetSites()
    {
        try
        {
            return await _service.GetSites();
        }
        catch (Exception e)
        {
            throw(e);
        }
    }

    [HttpGet]
    [Route("GetCurrentlyDefinedNetworks")]
    public async Task<List<Network>> GetCurrentlyDefinedNetworks()
    {
        try
        {
            return await _service.GetCurrentlyDefinedNetworks();
        }
        catch (Exception e)
        {
            throw(e);
        }
    }

    [HttpGet]
    [Route("GetWirelessNetworks")]
    public async Task<List<WirelessNetwork>> GetWirelessNetworks()
    {
        try
        {
            return await _service.GetWirelessNetworks();
        }
        catch (Exception e)
        {
            throw(e);
        }
    }

}
