using System.Collections.Generic;

namespace captive_portal_api.Models;
public class ResponseEnvelope<T> where T : BaseResponse
{
    public List<T>? data { get; set; }
    public Meta? meta { get; set; }
}
