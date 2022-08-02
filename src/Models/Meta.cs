using System.Text.Json;
using System.Text.Json.Serialization;

namespace captive_portal_api.Models;

public class Meta
{
    /// The result code indicating the successfulness of the request
    [JsonPropertyName("rc")]
    public string? ResultCode { get; set; }
}
