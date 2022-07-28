using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using captive_portal_api.Models;
using System.Threading.Tasks;
using System.Net;

namespace captive_portal_api.Services;

public class UnifiClientService
{
    private string? BaseUrl { get; set; }
    private LoginCredentials Credentials { get; set; }
    private string? SiteName { get; set; } = "default";
    private bool IsLoggedIn { get; set; }
    private string UDMProPrefix = "/proxy/network";

    public UnifiClientService(string user, string password, string baseUrl, string siteName, bool sslVerify = false)
    {
        Credentials = new LoginCredentials(user,password);
        BaseUrl = baseUrl;
        SiteName = siteName;

        IsLoggedIn = false;

    }

    public static void DisableSSL()
    {
    }

    public async Task<bool> Login()
    {
        if(IsLoggedIn)
        {  return true;}

        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>{return true;};
        using (var httpClient = new HttpClient(handler))
        {
            var loginEndPoint = "/api/auth/login";
            var url = this.BaseUrl + loginEndPoint;

            var data = JsonSerializer.Serialize(Credentials);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync(url,content))
            {
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    this.IsLoggedIn = true;
                    return true;
                }
                return false;
            }
        }
    }

    public async Task<bool> Logout()
    {
        return false;
    }

    public async Task<string> AuthorizeGuest(string macAddress)
    {
        return "";
    }

    public async Task<string> DeAuthorizeGuest(string macAddress)
    {
        return "";

    }

}