using System;

namespace captive_portal_api.Models;

public class LoginCredentials
{
    public string username { get; set; }
    public string password { get; set; }

    public LoginCredentials(string user, string password)
    {
        username = user;
        this.password = password;
    }
}