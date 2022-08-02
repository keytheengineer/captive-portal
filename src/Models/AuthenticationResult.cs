using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace captive_portal_api.Models;
public class AuthenticationResult : BaseResponse
{
    [JsonPropertyName("unique_id")]
    public string? UniqueId { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("email_status")]
    public string? EmailStatus { get; set; }

    [JsonPropertyName("email_is_null")]
    public bool EmailIsNull { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("avatar_relative_path")]
    public string? AvatarRelativePath { get; set; }

    [JsonPropertyName("avatar_rpath2")]
    public string? AvatarRpath2 { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("employee_number")]
    public string? EmployeeNumber { get; set; }

    [JsonPropertyName("create_time")]
    public long CreateTime { get; set; }

    [JsonPropertyName("extras")]
    public Extras? Extras { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("local_account_exist")]
    public bool LocalAccountExist { get; set; }

    [JsonPropertyName("password_revision")]
    public long PasswordRevision { get; set; }

    [JsonPropertyName("sso_account")]
    public string? SsoAccount { get; set; }

    [JsonPropertyName("sso_uuid")]
    public string? SsoUuid { get; set; }

    [JsonPropertyName("sso_username")]
    public string? SsoUsername { get; set; }

    [JsonPropertyName("sso_picture")]
    public string? SsoPicture { get; set; }

    [JsonPropertyName("uid_sso_id")]
    public string? UidSsoId { get; set; }

    [JsonPropertyName("uid_sso_account")]
    public string? UidSsoAccount { get; set; }

    [JsonPropertyName("groups")]
    public Group[]? Groups { get; set; }

    [JsonPropertyName("roles")]
    public Role[]? Roles { get; set; }

    [JsonPropertyName("permissions")]
    public Permissions? Permissions { get; set; }

    [JsonPropertyName("scopes")]
    public string[]? Scopes { get; set; }

    [JsonPropertyName("cloud_access_granted")]
    public bool CloudAccessGranted { get; set; }

    [JsonPropertyName("update_time")]
    public long UpdateTime { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    [JsonPropertyName("nfc_token")]
    public string? NfcToken { get; set; }

    [JsonPropertyName("nfc_display_id")]
    public string? NfcDisplayId { get; set; }

    [JsonPropertyName("nfc_card_type")]
    public string? NfcCardType { get; set; }

    [JsonPropertyName("nfc_card_status")]
    public string? NfcCardStatus { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("isOwner")]
    public bool IsOwner { get; set; }

    [JsonPropertyName("isSuperAdmin")]
    public bool IsSuperAdmin { get; set; }

    [JsonPropertyName("isMember")]
    public bool IsMember { get; set; }

    [JsonPropertyName("maskedEmail")]
    public string? MaskedEmail { get; set; }

    [JsonPropertyName("deviceToken")]
    public string? DeviceToken { get; set; }

    [JsonPropertyName("ssoAuth")]
    public Extras? SsoAuth { get; set; }
}

public partial class Extras
{
}

public partial class Group
{
    [JsonPropertyName("unique_id")]
    public string? UniqueId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("up_id")]
    public string? UpId { get; set; }

    [JsonPropertyName("up_ids")]
    public string[]? UpIds { get; set; }

    [JsonPropertyName("system_name")]
    public string? SystemName { get; set; }

    [JsonPropertyName("create_time")]
    public DateTimeOffset CreateTime { get; set; }
}

public partial class Permissions
{
    [JsonPropertyName("access.management")]
    public string[]? AccessManagement { get; set; }

    [JsonPropertyName("connect.management")]
    public string[]? ConnectManagement { get; set; }

    [JsonPropertyName("led.management")]
    public string[]? LedManagement { get; set; }

    [JsonPropertyName("network.management")]
    public string[]? NetworkManagement { get; set; }

    [JsonPropertyName("protect.management")]
    public string[]? ProtectManagement { get; set; }

    [JsonPropertyName("system.management.location")]
    public string[]? SystemManagementLocation { get; set; }

    [JsonPropertyName("system.management.user")]
    public string[]? SystemManagementUser { get; set; }

    [JsonPropertyName("talk.management")]
    public string[]? TalkManagement { get; set; }
}

public partial class Role
{
    [JsonPropertyName("unique_id")]
    public string? UniqueId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("system_role")]
    public bool SystemRole { get; set; }

    [JsonPropertyName("system_key")]
    public string? SystemKey { get; set; }

    [JsonPropertyName("level")]
    public long Level { get; set; }

    [JsonPropertyName("create_time")]
    public DateTimeOffset CreateTime { get; set; }

    [JsonPropertyName("update_time")]
    public DateTimeOffset UpdateTime { get; set; }

}
