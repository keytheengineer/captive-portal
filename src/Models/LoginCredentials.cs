using System;
using System.Text.Json.Serialization;

namespace captive_portal_api.Models;

public class LoginCredentials
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    public LoginCredentials(string? username, string? password)
    {
        this.Username = username;
        this.Password = password;
    }
}