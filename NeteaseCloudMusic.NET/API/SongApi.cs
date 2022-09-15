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

    public async Task DownloadSongAsync(long[] id,
        int bitRate = 320000)
    {
        if (!Directory.Exists("Music"))
        {
            Directory.CreateDirectory("Music");
        }
        var urls = await GetSongUrlAsync(id, bitRate);
        foreach (var url in urls)
        {
            var bytes = await _normalClient.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync($"Music/{id}.mp3", bytes);
        }
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

    public async Task GetSongDetailAsync (long id)
    {
        await GetSongDetailAsync(new []{id});
    }
    public async Task GetSongDetailAsync(params long[] ids)
    {
        var aa =
            $"[{string.Join(',', ids.Select(s => $"{{\"id\":{s}}}"))}]";
        Console.WriteLine(aa);
        Console.WriteLine(JsonSerializer.Serialize(new
                {
                    c = JsonSerializer.Serialize( ids.Select(s => new {id = s}))
                }));
        var a = 
            await RequestAsync("https://music.163.com/api/v3/song/detail", 
                HttpMethod.Post, 
                new
                {
                    c = aa
                }, 
                new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi
                });
        Console.WriteLine(await a.Content.ReadAsStringAsync());
    }
}