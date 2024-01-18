using System.Text.Json.Serialization;

namespace Websocket.Services.Dto;

public class ResponseDto
{
    [JsonPropertyName("data")]
    public IReadOnlyList<Data> Data { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
public class Data
{
    [JsonPropertyName("s")]
    public string Symbol { get; set; }

    [JsonPropertyName("p")]
    public decimal LastPrice { get; set; }

    [JsonPropertyName("t")]
    public long UNIXTimestamp { get; set; }

    [JsonPropertyName("v")]
    public decimal Volume { get; set; }
}