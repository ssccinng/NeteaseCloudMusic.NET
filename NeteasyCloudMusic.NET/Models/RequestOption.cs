namespace NeteasyCloudMusic.NET.Models;

public class RequestOption
{
    /// <summary>
    /// 加密方式
    /// </summary>
    public CryptoType Crypto { get; set; }
    /// <summary>
    /// Cookie数据
    /// </summary>
    public Dictionary<string, string> Cookie { get; set; }
    /// <summary>
    /// 真实IP
    /// </summary>
    public string? RealIP { get; set; }
    /// <summary>
    /// IP
    /// </summary>
    public string? IP { get; set; }
    /// <summary>
    /// 代理
    /// </summary>
    public string? Proxy { get; set; }
    
    
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
    
}