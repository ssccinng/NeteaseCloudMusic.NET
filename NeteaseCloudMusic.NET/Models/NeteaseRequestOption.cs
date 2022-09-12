namespace NeteaseCloudMusic.NET.Models;

public class NeteaseRequestOption
{
    /// <summary>
    /// 加密方式
    /// </summary>
    public CryptoType Crypto { get; set; }

    /// <summary>
    /// Cookie数据
    /// </summary>
    public Dictionary<string, string> Cookie { get; set; } =
        null;

    /// <summary>
    /// 真实IP
    /// </summary>
    public string? RealIP { get; set; } = null;

    /// <summary>
    /// IP
    /// </summary>
    public string? IP { get; set; } = null;

    /// <summary>
    /// 代理
    /// </summary>
    public string? Proxy { get; set; } = null;
    /// <summary>
    /// UserAgent类型
    /// </summary>
    public UAType UaType { get; set; }
}

public enum CryptoType
{
    Weapi,
    Eapi,
    Linuxapi,
    Api
}

public enum UAType
{
    PC,
    Mobile,
    Unkown
    
}