using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using captive_portal_api.Models;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace captive_portal_api.Services;
public class UnifiApiService 
{
    private HttpClient Client;
    private readonly ILogger<UnifiApiService> Logger;
    private LoginCredentials Credentials;
    public Uri BaseUri { get; private set; }
    public string SiteId { get; private set; } = "default";
    public bool IsAuthenticated { get; private set; } = false;
    public UnifiApiService(IHttpClientFactory httpClientFactory, ILogger<UnifiApiService> logger, IConfiguration config)
    {
        Client = httpClientFactory.CreateClient("UnifiApiClient");
        Logger = logger;
        BaseUri = new Uri(config["UNIFI_BASEURI"]); //todo environment Variable
        Credentials = new LoginCredentials(config["UNIFI_USERNAME"],config["UNIFI_PASSWORD"]); //todo environment variable
    }
    public async Task<bool>Reauthenticate()
    {
        if(string.IsNullOrEmpty(Credentials.Username) || string.IsNullOrEmpty(Credentials.Password))
        {
            throw new InvalidOperationException("No cached credentials yet. Call Authenticate first.");
        }
        await Login();
        if(!IsAuthenticated)
        {
            return false;
        }
        return true;
    }
    public async Task<AuthenticationResult> Login()
    {
        // Send an authentication request
        var authUri = new Uri(BaseUri, "/api/auth/login");
        var stringContent = new StringContent(JsonSerializer.Serialize(Credentials),Encoding.UTF8,"application/json");
        using(var request = new HttpRequestMessage(HttpMethod.Post, authUri))
        {
            request.Content = stringContent;
            request.Headers.Add("Accept", "application/json, text/plain, */*");        
            try
            {
                var responseMessage = await Client.SendAsync(request);
                if(responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    IsAuthenticated = true;
                }
                var result = await responseMessage.Content.ReadAsStringAsync();
                var authResult = JsonSerializer.Deserialize<AuthenticationResult>(result) ?? new AuthenticationResult();
                return authResult;
            }
            catch(HttpRequestException e)
            {
                Logger.LogError(e.ToString());
                throw(e);
            }
        }
    }
    public async Task<bool> Logout()
    {
        // Create a session towards the UniFi Controller
        var logoutUri = new Uri(BaseUri, "/api/logout");

        using(var request = new HttpRequestMessage(HttpMethod.Post, logoutUri))
        {
            request.Headers.Add("Accept", "application/json, text/plain, */*");        
            try
            {
                var responseMessage = await Client.SendAsync(request);
                if(responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    Logger.LogInformation(await responseMessage.Content.ReadAsStringAsync());
                    IsAuthenticated = false;
                    return true;
                }
                return false;
            }
            catch(HttpRequestException e)
            {
                Logger.LogError(e.ToString());
                throw(e);
            }
        }
    }
    //wrapper for a get request to ensure still logged in and then make the get request
    private async Task<string> EnsureAuthenticatedGetRequest(Uri uri)
    {
        // Ensure this session is authenticated
        if (!IsAuthenticated)
        {
            if (string.IsNullOrEmpty(Credentials.Username) || string.IsNullOrEmpty(Credentials.Password))
            {
                throw new InvalidOperationException("No active connection yet and unable to reauthenticate due to missing credentials. Call Authenticate first.");
            }
            if(!await Reauthenticate())
            {
                throw new InvalidOperationException("No active connection yet and unable to reauthenticate using cached credentials. Call Authenticate first.");
            }
        }
        try
        {
            var httpResponseMessage = await Client.GetAsync(uri);
            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            return result;
        }
        catch(HttpRequestException e) when (e.Message.Contains("401"))
        {
            if (IsAuthenticated)
            {
                if (!await Reauthenticate())
                {
                    throw new InvalidOperationException("Unable to reauthenticate using cached credentials. Call Authenticate first.");
                }
                var httpResponseMessage = await Client.GetAsync(uri);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();
                return result;
            }
            throw(e);
        }
    }
    //wrapper for a post request to ensure still logged in and then make the post request
    private async Task<string> EnsureAuthenticatedPostRequest(Uri uri, string postData)
    {
        if (!IsAuthenticated)
        {
            if (string.IsNullOrEmpty(Credentials.Username) || string.IsNullOrEmpty(Credentials.Password))
            {
                throw new InvalidOperationException("No active connection yet and unable to reauthenticate due to missing credentials. Call Authenticate first.");
            }
            if (!await Reauthenticate())
            {
                throw new InvalidOperationException("No active connection yet and unable to reauthenticate using cached credentials. Call Authenticate first.");
            }
        }

        var content = new StringContent(postData, Encoding.UTF8, "application/json");
        try
        {
            await Logout();
            await Login();
            var responseMessage = await Client.PostAsync(uri, content);
            if(responseMessage.StatusCode == HttpStatusCode.OK)
            {
                IsAuthenticated = true;
            }
            var result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }
        catch(HttpRequestException e) when (e.Message.Contains("401"))
        {
            Logger.LogError(e.ToString());
            if (IsAuthenticated)
            {
                if (!await Reauthenticate())
                {
                    throw new InvalidOperationException("Unable to reauthenticate using cached credentials. Call Authenticate first.");
                }
                var responseMessage = await Client.PostAsync(uri, content);
                if(responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    IsAuthenticated = true;
                }
                var result = await responseMessage.Content.ReadAsStringAsync();
                return result;
            }
            throw(e);
        }
        
    }
    public async Task<List<Client>> GetActiveClients()
    {
        var unifiUri = new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/stat/sta");
        var resultString = await EnsureAuthenticatedGetRequest(unifiUri);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<Client>>(resultString);
        return resultJson.data;
    }
    public async Task<List<Client>> GetAllClients()
    {
        var unifiUri = new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/stat/alluser");
        var resultString = await EnsureAuthenticatedGetRequest(unifiUri);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<Client>>(resultString);
        return resultJson.data;
    }
    public async Task<List<Device>> GetDevices()
    {
        var unifiUri = new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/stat/device");
        var resultString = await EnsureAuthenticatedGetRequest(unifiUri);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<Device>>(resultString);
        return resultJson.data;
    }
    public async Task<List<Site>> GetSites()
    {
        var unifiUri = new Uri(BaseUri, $"/proxy/network/api/self/sites");
        var resultString = await EnsureAuthenticatedGetRequest(unifiUri);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<Site>>(resultString);
        return resultJson.data;
    }
    public async Task<List<Network>> GetCurrentlyDefinedNetworks() 
    {
        var unifiUri = new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/rest/networkconf");
        var resultString = await EnsureAuthenticatedGetRequest(unifiUri);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<Network>>(resultString);
        return resultJson.data;
    }
    public async Task<List<WirelessNetwork>>GetWirelessNetworks() 
    {
        var unifiUri = new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/rest/wlanconf");
        var resultString = await EnsureAuthenticatedGetRequest(unifiUri);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<WirelessNetwork>>(resultString);
        return resultJson.data;
    }
    public async Task<List<ClientSession>> GetClientHistory(string macAddress, int limit = 5)
    {
        string payload = JsonSerializer.Serialize(new
        {
            _limit = limit,
            _sort = "-assoc_time",
            mac = macAddress
        });
        var resultString = await EnsureAuthenticatedPostRequest(new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/stat/session"),payload);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<ClientSession>>(resultString);
        return resultJson.data;
    }
    public async Task<ResponseEnvelope<Client>> BlockClient(Client client)
    {
        return await BlockClient(client.MacAddress);
    }
    public async Task<ResponseEnvelope<Client>> BlockClient(string macAddress)
    {
        string payload = JsonSerializer.Serialize(new
        {
            cmd = Commands.BlockSta,
            mac = macAddress
        });
        var resultString = await EnsureAuthenticatedPostRequest(new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/cmd/stamgr"),payload);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<Client>>(resultString) ?? new ResponseEnvelope<Client>();
        return resultJson;
    }
    public async Task<ResponseEnvelope<Client>> AuthorizeGuest(Client client)
    {
        if(string.IsNullOrEmpty(client.MacAddress))
        {
            throw new NullReferenceException("Client mac address doesnt exist");
        }
        return await AuthorizeGuest(client.MacAddress);
    }
    public async Task<ResponseEnvelope<Client>> AuthorizeGuest(string macAddress)
    {
        string payload = JsonSerializer.Serialize(new
        {
            cmd = Commands.AuthorizeGuest,
            mac = macAddress
        });
        var resultString = await EnsureAuthenticatedPostRequest(new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/cmd/stamgr"),payload);
        var clients = JsonSerializer.Deserialize<ResponseEnvelope<Client>>(resultString) ?? new ResponseEnvelope<Client>();
        return clients;
    }
    public async Task<ResponseEnvelope<Client>> UnauthorizeGuest(string macAddress)
    {
        string payload = JsonSerializer.Serialize(new
        {
            cmd = Commands.UnauthorizeGuest,
            mac = macAddress
        });
        var resultString = await EnsureAuthenticatedPostRequest(new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/cmd/stamgr"),payload);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<Client>>(resultString) ?? new ResponseEnvelope<Client>();
        return resultJson;
    }
    public async Task<ResponseEnvelope<Client>> UnblockClient(Client client)
    {
        return await UnblockClient(client.MacAddress);
    }
    public async Task<ResponseEnvelope<Client>> UnblockClient(string macAddress)
    {
        string payload = JsonSerializer.Serialize(new
        {
            cmd = Commands.UnblockSta,
            mac = macAddress
        });
        var resultString = await EnsureAuthenticatedPostRequest(new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/cmd/stamgr"),payload);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<Client>>(resultString) ?? new ResponseEnvelope<Client>();
        return resultJson;
    }
    public async Task<ResponseEnvelope<Client>> RemoveClients(string[] macArray)
    {
        string payload = JsonSerializer.Serialize(new
        {
            cmd = Commands.ForgetSta,
            macs = macArray
        });
        var resultString = await EnsureAuthenticatedPostRequest(new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/cmd/stamgr"), payload);

        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<Client>>(resultString) ?? new ResponseEnvelope<Client>();
        return resultJson;
    }
    public async Task<bool> ReconnectClient(Client client)
    {
        return await ReconnectClient(client.MacAddress);
    }
    public async Task<bool> ReconnectClient(string macAddress)
    {
        string payload = JsonSerializer.Serialize(new
        {
            cmd = Commands.KickSta,
            mac = macAddress
        });
        var resultString = await EnsureAuthenticatedPostRequest(new Uri(BaseUri, $"/proxy/network/api/s/{SiteId}/cmd/stamgr"), payload);
        var resultJson = JsonSerializer.Deserialize<ResponseEnvelope<BaseResponse>>(resultString) ?? new ResponseEnvelope<BaseResponse>();
        return resultJson.meta != null ? resultJson.meta.ResultCode.Equals("ok", StringComparison.InvariantCultureIgnoreCase) : false;
    }
}

