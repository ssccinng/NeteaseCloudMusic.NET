using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.Json;
using NeteaseCloudMusic.NET.Models;
using NeteaseCloudMusic.NET.Utils;
using static System.Text.Json.JsonSerializer;

namespace NeteaseCloudMusic.NET;


public partial class NeteasyCloudClient
{
    private HttpClient _neteaseClient;
    private HttpClient _normalClient = new HttpClient();
    private static string _header = @"Accept: */*
            Accept-Language: zh-CN,zh;q=0.8,gl;q=0.6,zh-TW;q=0.4
            Connection: keep-alive
            Host: music.163.com
            Referer: http://music.163.com/search/
            User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
    public NeteasyCloudClient()
    {
        _neteaseClient = new HttpClient
        {
            BaseAddress = new Uri("https://music.163.com/")
        };
        foreach (var item in _header.Split('\n'))
        {
            var data = item.Split(": ");
            _neteaseClient.DefaultRequestHeaders.Add(data[0].Trim(), data[1].Trim());
        }
    }
    /// <summary>
    /// 整合请求数据
    /// </summary>
    /// <param name="url"></param>
    /// <param name="method"></param>
    /// <param name="data"></param>
    /// <param name="requestOption"></param>
    public async Task RequestAsync(string url, HttpMethod method, object data, 
        RequestOption requestOption)
    {
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage
        {
            Method = method,
            
        };
        if (url.Contains("music.163.com"))
        {
            httpRequestMessage.Headers.Add("Referer", "https://music.163.com");
        }

        if (requestOption.IP != null)
        {
            httpRequestMessage.Headers.Add("X-Real-IP", requestOption.IP);
            httpRequestMessage.Headers.Add("X-Forwarded-For", requestOption.IP);
        }

        if (requestOption.Cookie != null)
        {
            // 添加cookie
        }

        httpRequestMessage.Headers.TryGetValues("Cookie", out var cookie);
        switch (requestOption.Crypto)
        {
            case CryptoType.Weapi:
                // 加密后的数据
                var eData = 
                    Encrypt.EncryptedRequestWeapi(Serialize(data));
                // 需要提取csrf
                
                if ()
                {
                    
                }
                
                httpRequestMessage.Content = new FormUrlEncodedContent(eData);
                break;
            case CryptoType.Eapi:
                break;
            case CryptoType.Api:
                break;
            default:
                break;
        }

        await _neteaseClient.SendAsync(httpRequestMessage);
    }
    /// <summary>
    /// 解压GZip字符串
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string DecompressionGzip(string input)
    {
        return DecompressionGzip(new MemoryStream(Encoding.UTF8.GetBytes(input)));
    }
    /// <summary>
    /// 解压GZip流
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string DecompressionGzip(Stream input)
    {
        using GZipStream gZipStream = new GZipStream(input, CompressionMode.Decompress);
        using var resultStream = new MemoryStream();
        gZipStream.CopyTo(resultStream);
        return Encoding.UTF8.GetString(resultStream.ToArray());
    }



    private static string[] _mobileUAs = new[]
    {
        "Mozilla/5.0 (iPhone; CPU iPhone OS 13_5_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.1.1 Mobile/15E148 Safari/604.1",
        "Mozilla/5.0 (iPhone; CPU iPhone OS 14_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.",
        // iOS with qq micromsg
        "Mozilla/5.0 (iPhone; CPU iPhone OS 13_5_1 like Mac OS X) AppleWebKit/602.1.50 (KHTML like Gecko) Mobile/14A456 QQ/6.5.7.408 V1_IPH_SQ_6.5.7_1_APP_A Pixel/750 Core/UIWebView NetType/4G Mem/103",
        "Mozilla/5.0 (iPhone; CPU iPhone OS 13_5_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/7.0.15(0x17000f27) NetType/WIFI Language/zh",
        // Android -> Huawei Xiaomi
        "Mozilla/5.0 (Linux; Android 9; PCT-AL10) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.64 HuaweiBrowser/10.0.3.311 Mobile Safari/537.36",
        "Mozilla/5.0 (Linux; U; Android 9; zh-cn; Redmi Note 8 Build/PKQ1.190616.001) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/71.0.3578.141 Mobile Safari/537.36 XiaoMi/MiuiBrowser/12.5.22",
        // Android + qq micromsg
        "Mozilla/5.0 (Linux; Android 10; YAL-AL00 Build/HUAWEIYAL-AL00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/78.0.3904.62 XWEB/2581 MMWEBSDK/200801 Mobile Safari/537.36 MMWEBID/3027 MicroMessenger/7.0.18.1740(0x27001235) Process/toolsmp WeChat/arm64 NetType/WIFI Language/zh_CN ABI/arm64",
        "Mozilla/5.0 (Linux; U; Android 8.1.0; zh-cn; BKK-AL10 Build/HONORBKK-AL10) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/66.0.3359.126 MQQBrowser/10.6 Mobile Safari/537.36",
    };

    private static string[] _pcUAs = new[]
    {
        "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.15; rv:80.0) Gecko/20100101 Firefox/80.0",
        "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.30 Safari/537.36",
        "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.1.2 Safari/605.1.15",
        // Windows 10 Firefox / Chrome / Edge
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:80.0) Gecko/20100101 Firefox/80.0",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.30 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/13.10586",
    };
    /// <summary>
    /// 根据种类随机选取ua
    /// </summary>
    /// <param name="uaType"></param>
    /// <returns></returns>
    public static string ChooseUserAgent(UAType uaType)
    {
        switch (uaType)
        {
            case UAType.Mobile:
                return _mobileUAs[Random.Shared.Next(_mobileUAs.Length)];
                
                // 考虑去提一个random的linq
                // _mobileUAs
                break;
            case UAType.PC:
                return _pcUAs[Random.Shared.Next(_mobileUAs.Length)];
                break;
            default:
                break;
        }
        return _pcUAs[Random.Shared.Next(_mobileUAs.Length)];

    } 
    
    
    
}