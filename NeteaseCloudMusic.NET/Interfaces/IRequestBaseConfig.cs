namespace NeteaseCloudMusic.NET.Interfaces;

public interface IRequestBaseConfig
{
    /// <summary>
    /// Cookies
    /// </summary>
    Dictionary<string, string> Cookies { get; set; }
    /// <summary>
    /// 真实IP
    /// </summary>
    string? RealIP { get; set; }
    
    /// <summary>
    /// 代理
    /// </summary>
    string? Proxy { get; set; }
}