﻿using System.Text.Json;
using System.Text.Json.Serialization;
using System;

namespace captive_portal_api.Models;
/// <summary>
/// Details of a UniFi device such as an access point or switch
/// </summary>
public class Device : BaseResponse
{
    /// <summary>
    /// Name of the device
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// MAC address of the device
    /// </summary>
    [JsonPropertyName("mac")]
    public string? MacAddress { get; set; }

    /// <summary>
    /// Boolean indicating if the device has been adopted by UniFi
    /// </summary>
    [JsonPropertyName("adopted")]
    public bool? Adpoted { get; set; }

    /// <summary>
    /// Serial Number of the device
    /// </summary>
    [JsonPropertyName("serial")]
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Device uptime in seconds
    /// </summary>
    [JsonPropertyName("uptime")]
    public long? Uptime { get; set; }

    /// <summary>
    /// Device uptime as a TimeSpan
    /// </summary>
    public TimeSpan? UptimeTimeSpan { get { return Uptime.HasValue ? (TimeSpan?)TimeSpan.FromSeconds(Uptime.Value) : null; } }

    /// <summary>
    /// Date and time at which the device started
    /// </summary>
    public DateTime? StartedAt { get { return Uptime.HasValue ? (DateTime?)DateTime.Now.AddSeconds(Uptime.Value * -1) : null; } }

    /// <summary>
    /// Device model
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// Device hostname
    /// </summary>
    [JsonPropertyName("hostname")]
    public string? Hostname { get; set; }

    /// <summary>
    /// Device Type
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Device Adopt IP
    /// </summary>
    [JsonPropertyName("adopt_ip")]
    public string? AdoptIP { get; set; }

    /// <summary>
    /// Unique device ID
    /// </summary>
    [JsonPropertyName("_id")]
    public string? Id { get; set; }
}

