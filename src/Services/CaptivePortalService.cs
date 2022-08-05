using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace captive_portal_api.Services;
public class CaptivePortalService 
{

    private readonly UnifiApiService unifiApiService;
    private readonly ILogger<CaptivePortalService> _logger;

    public CaptivePortalService(UnifiApiService service, ILogger<CaptivePortalService> logger)
    {
        unifiApiService = service;
        _logger = logger;
    }

    public async Task<bool> RequestGuestAccess(string ipAddress)
    {
        var client = (await unifiApiService.GetActiveClients())
            .FirstOrDefault(x => x.IpAddressInternal == ipAddress);
        if(client != null)
        {
            _logger.LogInformation($"Client Found. Hostname: {client.Hostname}, IpAddress: {client.IpAddressInternal}, MAC: {client.MacAddress}");
            // TODO send info to Home Assistant
            // TODO await verify with HA Admin
            var approved = true; //placeholder
            if(approved)
            {
                var authorizeGuestResult = (await unifiApiService.AuthorizeGuest(client)).data;
                var result = authorizeGuestResult != null ? authorizeGuestResult.First().IsAuthorized.GetValueOrDefault() : false;
                return result;
            }
            return false;
        }
        return false;


        //get mac and other info from IP address using UNIFI service
        //send this info to Home Assistant for authorization request
        //Authenticate user in unifi
        return true;

    }

    public bool IsGuestAlreadyAuthenticated(string ipAddress)
    {
        //check to see if the user is already an authenticated guest
        return false;
    }
}
