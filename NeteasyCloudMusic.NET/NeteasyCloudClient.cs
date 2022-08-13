using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.Json;
using NeteasyCloudMusic.NET.Models;

namespace NeteasyCloudMusic.NET;

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
        return String.Empty;
    }
    public static string DecompressionGzip(Stream input)
    {
        using GZipStream gZipStream = new GZipStream(input, CompressionMode.Decompress);
        using var resultStream = new MemoryStream();
        gZipStream.CopyTo(resultStream);
        return Encoding.UTF8.GetString(resultStream.ToArray());
        // using (var resultStream = new MemoryStream())
        // {
        //     gZipStream.CopyTo(resultStream);
        //     // var gfata = Encoding.UTF8.GetString(resultStream.ToArray());
        //     // var data1 = JsonDocument.Parse(gfata).RootElement;
        //     // return data1.GetProperty("data").GetProperty("user").GetProperty("result").GetProperty("rest_id").GetString();
        //     return Encoding.UTF8.GetString(resultStream.ToArray());
        // }
    }
}