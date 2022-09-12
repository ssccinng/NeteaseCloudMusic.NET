using System.Net.Http.Json;
using System.Text.Json;
using NeteaseCloudMusic.NET.Models;

namespace NeteaseCloudMusic.NET;

public partial class NeteasyCloudClient
{
    public async Task<string?> GetSongUrlAsync(long id,
        int bitRate = 320000)
    {
        var res = (await GetSongUrlAsync(new[] { id }, bitRate));
        if (res.Length > 0) return res[0];
        return null;
    }
    public async Task<string[]> GetSongUrlAsync(long[] id, int bitRate = 320000)
    {
        var data = await GetSongAsync(id, bitRate);
        return data.Data.Select(S => S.Url).ToArray();
    }

    public async Task<Song> GetSongAsync(long[] ids,
        int bitRate = 320000)
    {
        var a = 
            await RequestAsync("https://music.163.com/weapi/song/enhance/player/url", 
                HttpMethod.Post, 
                new
                {
                    ids = ids.Select(s => s.ToString()),
                    br = bitRate,
                    csrf_token = ""
                }, 
                new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi
                });
        var data = await a.Content.ReadFromJsonAsync<Song>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return data;
    }
}