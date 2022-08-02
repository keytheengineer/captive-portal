using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace captive_portal_api.Models;

// Defines one client session having been connected to the UniFi network
public class ClientSession : BaseResponse
{
    // Unique identifier of the session
    [JsonPropertyName("_id")]
    public string? Id { get; set; }
    // Seconds since January 1, 1970 when the client started this session. Use SessionStartedAt for a DateTime representing this value.
    [JsonPropertyName("assoc_time")]
    public long? SessionStartedAtRaw { get; set; }
    // DateTime when the client started this session
    [JsonIgnore]
    public DateTime? SessionStartedAt
    {
        get { return SessionStartedAtRaw.HasValue ? (DateTime?)new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(SessionStartedAtRaw.Value).ToLocalTime() : null; }
        set { SessionStartedAtRaw = value.HasValue ? (long?)Math.Floor((value.Value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) : null; }
    }
    // DateTime when the client ended this session. If this DateTime is close to the current date and time, it means that the session is still active.
    [JsonIgnore]
    public DateTime? SessionEndedAt => SessionStartedAt.HasValue && SessionDuration.HasValue ? (DateTime?) SessionStartedAt.Value.AddSeconds(SessionDuration.Value) : null;
    // Duration of the current session of the client in seconds
    [JsonPropertyName("duration")]
    public long? SessionDuration { get; set; }
    // Duration of the current session of the client as a TimeSpan
    public TimeSpan? SessionDurationTimeSpan { get { return SessionDuration.HasValue ? (TimeSpan?) TimeSpan.FromSeconds(SessionDuration.Value) : null; } }
    // Amount of bytes received by the client through the UniFi network
    [JsonPropertyName("tx_bytes")]
    public long? TransmittedBytes{ get; set; }
    // Amount of bytes uploaded by the client through the UniFi network
    [JsonPropertyName("rx_bytes")]
    public long? ReceivedBytes { get; set; }
    // MAC Address of the client that was connected
    [JsonPropertyName("mac")]
    public string? ClientMacAddress { get; set; }
    // Name of the client as registered in UniFi
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    // Name of the device as broadcasted by the device itself
    [JsonPropertyName("hostname")]
    public string? Hostname { get; set; }
    // Was this client logged on through a guest network
    [JsonPropertyName("is_guest")]
    public bool? IsGuest { get; set; }
    // IP Address assigned to the client
    [JsonPropertyName("ip")]
    public string? IpAddress { get; set; }
    // Was this client wired to the UniFi network (true) or wirelessly connected (false)
    [JsonPropertyName("is_wired")]
    public bool IsWired { get; set; }
    // MAC Address of the Access Point the client was connected to
    [JsonPropertyName("ap_mac")]
    public string? AccessPointMacAddress { get; set; }
    // Meaning unknown
    [JsonPropertyName("o")]
    public string? O { get; set; }
    // Meaning unknown
    [JsonPropertyName("oid")]
    public string? Oid { get; set; }
}

