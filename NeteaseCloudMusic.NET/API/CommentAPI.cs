using NeteaseCloudMusic.NET.Models;

namespace NeteaseCloudMusic.NET;

public partial class NeteasyCloudClient
{
    /// <summary>
    /// 获取专辑评论
    /// </summary>
    /// <param name="rid"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="beforeTime"></param>
    /// <returns></returns>
    public async Task<object> GetAlbumCommentAsync(long rid, int limit = 50, int offset = 0, int beforeTime = 0)
    {
        return new { };
    }
    
    public async Task<object> GetMusicCommentAsync(long radioId, int limit = 50, int offset = 0, int beforeTime = 0)
    {
        var res = await RequestAsync($"https://music.163.com/weapi/v1/resource/comments/R_SO_4_{radioId}",
            HttpMethod.Post, new
            {
                rid = radioId,
                limit = limit,
                offset = offset,
                beforeTime = beforeTime
            }, new NeteaseRequestOption
            {
                Crypto = CryptoType.Weapi,
                Cookie = new Dictionary<string, string> { { "os", "pc" } },
            });
        return await res.Content.ReadAsStringAsync();
    }


}