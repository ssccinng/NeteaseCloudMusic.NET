using System.Text.Json.Serialization;

namespace NeteaseCloudMusic.NET.Models.Djradio;

public class Djradio
{
    /// <summary>
    /// 音乐总数
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
    /// <summary>
    /// 状态码
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }
    /// <summary>
    /// 电台音乐列表
    /// </summary>
    [JsonPropertyName("programs")]
    public List<Program> Programs { get; set; }
    /// <summary>
    /// 是否还有音乐
    /// </summary>
    [JsonPropertyName("more")]
    public bool More { get; set; }
}