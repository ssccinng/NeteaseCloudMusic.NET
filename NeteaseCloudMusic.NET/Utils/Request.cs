using System.Dynamic;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using NeteaseCloudMusic.NET.Models;
using NeteaseCloudMusic.NET.Utils;
using static System.Text.Json.JsonSerializer;

namespace NeteaseCloudMusic.NET;

// Requests
public partial class NeteasyCloudClient
{
    public const string Config =
        @"{
          ""anonymous_token"": """",
          ""resourceTypeMap"": {
            ""0"": ""R_SO_4_"",
            ""1"": ""R_MV_5_"",
            ""2"": ""A_PL_0_"",
            ""3"": ""R_AL_3_"",
            ""4"": ""A_DJ_1_"",
            ""5"": ""R_VI_62_"",
            ""6"": ""A_EV_2_""
          }
        }";


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
    public static string ChooseUserAgent(
        UAType uaType = UAType.Unkown)
    {
        switch (uaType)
        {
            case UAType.Mobile:
                return _mobileUAs[
                    Random.Shared.Next(_mobileUAs.Length)];

                // 考虑去提一个random的linq
                // _mobileUAs
                break;
            case UAType.PC:
                return _pcUAs[
                    Random.Shared.Next(_pcUAs.Length)];
                break;
            default:

                break;
        }

        return _pcUAs
            [Random.Shared.Next(_mobileUAs.Length)];
    }


    public async Task<HttpResponseMessage> RequestAsync
    (
        string url,
        HttpMethod method, 
        object data,
        NeteaseRequestOption neteaseRequestOption)
    {
        JsonNode? jData = SerializeToNode(data);
        // 设置UA
        HttpRequestMessage httpRequestMessage =
            new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method,
                Headers =
                {
                    {
                        "User-Agent",
                        ChooseUserAgent(neteaseRequestOption
                            .UaType)
                    }
                }
            };

        if (method == HttpMethod.Post)
        {
            // C# HttpClient无法设置ContentType 会根据内容自动实现
            // httpRequestMessage.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        }

        // 设置Referer
        if (url.Contains("music.163.com"))
        {
            httpRequestMessage.Headers.Add("Referer",
                "https://music.163.com");
        }

        string? ip = neteaseRequestOption.IP ??
                     neteaseRequestOption.RealIP ?? null
            ;
        // 设置IP
        // if (requestOption.IP != null)
        if (ip != null)
        {
            httpRequestMessage.Headers.Add("X-Real-IP",
                ip);
            httpRequestMessage.Headers.Add(
                "X-Forwarded-For", ip);
        }

        // 设置Cookie 如果cookie是类 则走上面，如果是string 则走下面
        if (neteaseRequestOption.Cookie != null)
        {
            if (!neteaseRequestOption.Cookie.ContainsKey(
                    "MUSIC_U"))
            {
                if (!neteaseRequestOption.Cookie
                        .ContainsKey(
                            "MUSIC_A"))
                {
                    neteaseRequestOption.Cookie["MUSIC_A"] =
                        // Config;
                        "";
                }
            }

            // 添加cookie
            httpRequestMessage.Headers.Add("Cookie",
                 string.Join(';', neteaseRequestOption.Cookie.Select
                    (s => $"{s.Key}={s.Value}")));
        }
        else
        {
            // neteaseRequestOption.Cookie = ""
        }

        httpRequestMessage.Headers.TryGetValues("Cookie",
            out var cookie);
        // 根据不同的协议 设置不同的加密
        switch (neteaseRequestOption.Crypto)
        {
            case CryptoType.Weapi:
                if (cookie != null)
                {
                    // 搜素cookie中的csrf
                    var match = Regex.Match(cookie.First(),
                        "/_csrf=([^(;|$)]+)/");
                    if (match.Success)
                    {
                        // data加个字段
                        var csrf = match.Groups[1].Value;
                        // var nData =
                        //     Encrypt.EncryptedRequestWeapi(
                        //         data.ToString());
                        jData["csrf"] = csrf;
                        // 偶？ 终于增加了这个功能

                    }
                }

                // 加密后的数据
                var eData =
                    Encrypt.EncryptedRequestWeapi(
                        jData.ToJsonString());
                // 需要提取csrf


                httpRequestMessage.Content =
                    new FormUrlEncodedContent(eData);
                break;
            case CryptoType.Linuxapi:
                break;
            case CryptoType.Eapi:
                break;
            case CryptoType.Api:
                break;
            default:
                break;
        }

        var aa = await _neteaseClient.SendAsync(httpRequestMessage);
        return aa;
    }
}