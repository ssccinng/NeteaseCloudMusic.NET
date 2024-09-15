using NeteaseCloudMusic.NET.Models;
using System.Net.Http.Json;

namespace NeteaseCloudMusic.NET;

public partial class NeteasyCloudClient
{
    public async Task<Song1[]> SearchMusicAsync(string kw, int limit = 50, int offset = 0, int type = 1)
    {
        var res = await RequestAsync($"https://music.163.com/weapi/cloudsearch/get/web",
            HttpMethod.Post, new
            {
                s = kw,
                type = 1, // 1: 单曲, 10: 专辑, 100: 歌手, 1000: 歌单, 1002: 用户, 1004: MV, 1006: 歌词, 1009: 电台, 1014: 视频

                total = true,

                limit = limit,
                offset = offset,

            }, new NeteaseRequestOption
            {
                Crypto = CryptoType.Weapi,
                Cookie = new Dictionary<string, string> { { "os", "pc" } },
            });
        return (await res.Content.ReadFromJsonAsync<Rootobject>()).result.songs;
    }
}



public class Rootobject
{
    public Result result { get; set; }
    public long? code { get; set; }
}

public class Result
{
    public object searchQcReminder { get; set; }
    public Song1[] songs { get; set; }
    public long? songCount { get; set; }
}

public class Song1
{
    public string name { get; set; }
    public long? id { get; set; }
    public long? pst { get; set; }
    public long? t { get; set; }
    public Ar[] ar { get; set; }
    public string[] alia { get; set; }
    public float? pop { get; set; }
    public long? st { get; set; }
    public string rt { get; set; }
    public long? fee { get; set; }
    public long? v { get; set; }
    public object crbt { get; set; }
    public string cf { get; set; }
    public Al al { get; set; }
    public long? dt { get; set; }
    public H h { get; set; }
    public M m { get; set; }
    public L l { get; set; }
    public Sq sq { get; set; }
    public Hr hr { get; set; }
    public object a { get; set; }
    public string cd { get; set; }
    public long? no { get; set; }
    public object rtUrl { get; set; }
    public long?ftype { get; set; }
    public object[] rtUrls { get; set; }
    public long?djId { get; set; }
    public long?copyright { get; set; }
    public long?s_id { get; set; }
    public long?mark { get; set; }
    public long?originCoverType { get; set; }
    public object originSongSimpleData { get; set; }
    public object tagPicList { get; set; }
    public bool resourceState { get; set; }
    public long?version { get; set; }
    public object songJumpInfo { get; set; }
    public object entertainmentTags { get; set; }
    public long?single { get; set; }
    public object noCopyrightRcmd { get; set; }
    public long?rtype { get; set; }
    public object rurl { get; set; }
    public long?mst { get; set; }
    public long?cp { get; set; }
    public long?mv { get; set; }
    public long?publishTime { get; set; }
    public string[] tns { get; set; }
    public Privilege privilege { get; set; }
}

public class Al
{
    public long?id { get; set; }
    public string name { get; set; }
    public string picUrl { get; set; }
    public object[] tns { get; set; }
    public string pic_str { get; set; }
    public long?pic { get; set; }
}

public class H
{
    public long?br { get; set; }
    public long?fid { get; set; }
    public long?size { get; set; }
    public float vd { get; set; }
    public long?sr { get; set; }
}

public class M
{
    public long?br { get; set; }
    public long?fid { get; set; }
    public long?size { get; set; }
    public float vd { get; set; }
    public long?sr { get; set; }
}

public class L
{
    public long?br { get; set; }
    public long?fid { get; set; }
    public long?size { get; set; }
    public float vd { get; set; }
    public long?sr { get; set; }
}

public class Sq
{
    public long?br { get; set; }
    public long?fid { get; set; }
    public long?size { get; set; }
    public float vd { get; set; }
    public long?sr { get; set; }
}

public class Hr
{
    public long?br { get; set; }
    public long?fid { get; set; }
    public long?size { get; set; }
    public float vd { get; set; }
    public long?sr { get; set; }
}

public class Privilege
{
    public long?id { get; set; }
    public long?fee { get; set; }
    public long?payed { get; set; }
    public long?st { get; set; }
    public long?pl { get; set; }
    public long?dl { get; set; }
    public long?sp { get; set; }
    public long?cp { get; set; }
    public long?subp { get; set; }
    public bool cs { get; set; }
    public long?maxbr { get; set; }
    public long?fl { get; set; }
    public bool toast { get; set; }
    public long?flag { get; set; }
    public bool preSell { get; set; }
    public long?playMaxbr { get; set; }
    public long?downloadMaxbr { get; set; }
    public string maxBrLevel { get; set; }
    public string playMaxBrLevel { get; set; }
    public string downloadMaxBrLevel { get; set; }
    public string plLevel { get; set; }
    public string dlLevel { get; set; }
    public string flLevel { get; set; }
    public object rscl { get; set; }
    public Freetrialprivilege freeTrialPrivilege { get; set; }
    public long?rightSource { get; set; }
    public Chargeinfolist[] chargeInfoList { get; set; }
}

public class Freetrialprivilege
{
    public bool resConsumable { get; set; }
    public bool userConsumable { get; set; }
    public long?listenType { get; set; }
    public long?cannotListenReason { get; set; }
}

public class Chargeinfolist
{
    public long?rate { get; set; }
    public object chargeUrl { get; set; }
    public object chargeMessage { get; set; }
    public long?chargeType { get; set; }
}

public class Ar
{
    public long?id { get; set; }
    public string name { get; set; }
    public string[] tns { get; set; }
    public string[] alias { get; set; }
    public string[] alia { get; set; }
}
