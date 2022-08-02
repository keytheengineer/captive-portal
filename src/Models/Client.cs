using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace captive_portal_api.Models;

public class Client : BaseResponse
{
    [JsonPropertyName("_id")]
    public string? Id { get; set; }

    [JsonPropertyName("_is_guest_by_uap")]
    public bool? IsGuestByUap { get; set; }

    [JsonPropertyName("_last_seen_by_uap")]
    public long? LastSeenByUapRaw { get; set; }

    [JsonIgnore]
    public DateTime? LastSeenByUap
    {
        get { return LastSeenByUapRaw.HasValue ? (DateTime?)new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(LastSeenByUapRaw.Value).ToLocalTime() : null; }
        set { LastSeenByUapRaw = value.HasValue ? (long?)Math.Floor((value.Value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) : null; }
    }

    [JsonPropertyName("_uptime_by_uap")]
    public long? UptimeByUapRaw { get; set; }

    [JsonIgnore]
    public TimeSpan? UptimeByUap
    {
        get { return UptimeByUapRaw.HasValue ? (TimeSpan?)TimeSpan.FromSeconds(UptimeByUapRaw.Value) : null; }
        set { UptimeByUapRaw = value.HasValue ? (long?)value.Value.TotalSeconds : null; }
    }

    [JsonPropertyName("ap_mac")]
    public string? AccessPointMacAddress { get; set; }

    [JsonPropertyName("assoc_time")]
    public long? AssociatedTimeRaw { get; set; }

    [JsonIgnore]
    public DateTime? AssociatedTime
    {
        get { return AssociatedTimeRaw.HasValue ? (DateTime?)new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(AssociatedTimeRaw.Value).ToLocalTime() : null; }
        set { AssociatedTimeRaw = value.HasValue ? (long?)Math.Floor((value.Value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) : null; }
    }

    [JsonPropertyName("authorized")]
    public bool? IsAuthorized { get; set; }

    [JsonPropertyName("authorized_by")]
    public string? AuthorizedBy{ get; set; }

    [JsonPropertyName("bssid")]
    public string? BssId { get; set; }

    [JsonPropertyName("bytes-r")]
    public long? BytesReceived { get; set; }

    [JsonPropertyName("ccq")]
    public int? Ccq { get; set; }

    [JsonPropertyName("channel")]
    public int? Channel { get; set; }

    [JsonPropertyName("essid")]
    public string? EssId { get; set; }

    [JsonPropertyName("first_seen")]
    public long? FirstSeenRaw { get; set; }

    [JsonIgnore]
    public DateTime? FirstSeen
    {
        get { return FirstSeenRaw.HasValue ? (DateTime?)new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(FirstSeenRaw.Value).ToLocalTime() : null; }
        set { FirstSeenRaw = value.HasValue ? (long?)Math.Floor((value.Value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) : null; }
    }

    [JsonPropertyName("hostname")]
    public string? Hostname { get; set; }

    [JsonPropertyName("idletime")]
    public long? IdleTimeRaw { get; set; }

    [JsonIgnore]
    public TimeSpan? IdleTime
    {
        get { return IdleTimeRaw.HasValue ? (TimeSpan?)TimeSpan.FromSeconds(IdleTimeRaw.Value) : null; }
        set { IdleTimeRaw = value.HasValue ? (long?)value.Value.TotalSeconds : null; }
    }

    [JsonPropertyName("ip")]
    public string? IpAddress { get; set; }

    [JsonPropertyName("is_guest")]
    public bool? IsGuest { get; set; }

    [JsonPropertyName("blocked")]
    public bool? IsBlocked { get; set; }

    [JsonPropertyName("is_wired")]
    public bool? IsWired { get; set; }

    [JsonPropertyName("last_seen")]
    public long? LastSeenRaw { get; set; }

    [JsonIgnore]
    public DateTime? LastSeen
    {
        get { return LastSeenRaw.HasValue ? (DateTime?)new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(LastSeenRaw.Value).ToLocalTime() : null; }
        set { LastSeenRaw = value.HasValue ? (long?)Math.Floor((value.Value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) : null; }
    }

    [JsonPropertyName("latest_assoc_time")]
    public long? LatestAssociationTimeRaw { get; set; }

    [JsonIgnore]
    public DateTime? LatestAssociationTime
    {
        get { return LatestAssociationTimeRaw.HasValue ? (DateTime?)new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(LatestAssociationTimeRaw.Value).ToLocalTime() : null; }
        set { LatestAssociationTimeRaw = value.HasValue ? (long?)Math.Floor((value.Value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) : null; }
    }

    [JsonPropertyName("mac")]
    public string? MacAddress { get; set; }

    [JsonPropertyName("name")]
    public string? FriendlyName { get; set; }

    [JsonPropertyName("noise")]
    public int? Noise { get; set; }

    [JsonPropertyName("noted")]
    public bool? IsNoted { get; set; }

    [JsonPropertyName("oui")]
    public string? Brand { get; set; }

    [JsonPropertyName("powersave_enabled")]
    public bool? IsPowersaveEnabled { get; set; }

    [JsonPropertyName("qos_policy_applied")]
    public bool? IsQosPolicyApplied { get; set; }

    [JsonPropertyName("radio")]
    public string? RadioBand { get; set; }

    [JsonPropertyName("radio_proto")]
    public string? RadioProtocol { get; set; }

    [JsonPropertyName("rssi")]
    public int? SignalStrength { get; set; }

    [JsonPropertyName("rx_bytes")]
    public long? ReceivedBytesAllTime { get; set; }

    [JsonPropertyName("rx_bytes-r")]
    public long? ReceivedBytesSession { get; set; }

    [JsonPropertyName("rx_packets")]
    public long? ReceivedPackets { get; set; }

    [JsonPropertyName("rx_rate")]
    public long? ReceivedRate { get; set; }

    [JsonPropertyName("signal")]
    public long? Signal { get; set; }

    [JsonPropertyName("start")]
    public long? Start { get; set; }

    public DateTime? StartDate => Start.HasValue ? new DateTime(1970, 1, 1).AddSeconds(Start.Value) : (DateTime?) null;

    [JsonPropertyName("end")]
    public long? End { get; set; }

    public DateTime? EndDate => End.HasValue ? new DateTime(1970, 1, 1).AddSeconds(End.Value) : (DateTime?)null;

    [JsonPropertyName("site_id")]
    public string? SiteId { get; set; }

    [JsonPropertyName("tx_bytes")]
    public long? TransmittedBytesAllTime { get; set; }

    [JsonPropertyName("tx_bytes-r")]
    public long? TransmittedBytesSession { get; set; }

    [JsonPropertyName("tx_packets")]
    public long? TransmittedPackets { get; set; }

    [JsonPropertyName("tx_power")]
    public long? TransmittedPower { get; set; }

    [JsonPropertyName("tx_rate")]
    public long? TransmittedRate { get; set; }

    [JsonPropertyName("uptime")]
    public long? UptimeRaw { get; set; }

    [JsonIgnore]
    public TimeSpan? Uptime
    {
        get { return UptimeRaw.HasValue ? (TimeSpan?)TimeSpan.FromSeconds(UptimeRaw.Value) : null; }
        set { UptimeRaw = value.HasValue ? (long?)value.Value.TotalSeconds : null; }
    }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("vlan")]
    public int? Vlan { get; set; }

    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("usergroup_id")]
    public string? UserGroupId { get; set; }

    public override string? ToString()
    {
        return FriendlyName ?? Hostname;
    }
}

