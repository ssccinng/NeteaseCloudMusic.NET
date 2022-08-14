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
    private HttpClient _neteasyClient;
    private HttpClient _normalClient = new HttpClient();
    private static string _header = @"Accept: */*
            Accept-Language: zh-CN,zh;q=0.8,gl;q=0.6,zh-TW;q=0.4
            Connection: keep-alive
            Host: music.163.com
            Referer: http://music.163.com/search/
            User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
    public NeteasyCloudClient()
    {
        _neteasyClient = new HttpClient
        {
            BaseAddress = new Uri("https://music.163.com/")
        };
        foreach (var item in _header.Split('\n'))
        {
            var data = item.Split(": ");
            _neteasyClient.DefaultRequestHeaders.Add(data[0].Trim(), data[1].Trim());
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
        switch (requestOption.Crypto)
        {
            case CryptoType.Weapi:
                // 加密后的数据
                var eData = 
                    Encrypt.EncryptedRequestWeapi(Serialize(data));
                httpRequestMessage.Content = new FormUrlEncodedContent(eData);
                break;
            case CryptoType.Eapi:
                break;
            case CryptoType.Api:
                break;
            default:
                break;
        }

        await _neteasyClient.SendAsync(httpRequestMessage);
    }
    
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
    
    
    
}