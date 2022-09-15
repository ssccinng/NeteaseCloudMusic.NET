using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NeteaseCloudMusic.NET.Models;
using NeteaseCloudMusic.NET.Models.Djradio;

namespace NeteaseCloudMusic.NET
{
    public partial class NeteasyCloudClient
    {
        public async Task<IEnumerable<MusicInfo>>
            GetDjradioListAsync(int id)
        {
            //return await GetDjradioListAsync($"https://music.163.com/djradio?id={id}&_hash=programlist&limit=10000");
            return await GetDjradioListAsync(
                $"djradio?id={id}&_hash=programlist&limit=10000");
        }

        public async Task<IEnumerable<MusicInfo>>
            GetDjradioListAsync(string url)
        {
            var res = await _neteaseClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                List<MusicInfo> musicInfos = new();
                var danier =
                    await res.Content
                        .ReadAsByteArrayAsync();
                var html = Encoding.UTF8.GetString(
                    await res.Content
                        .ReadAsByteArrayAsync());
                var matches = Regex.Matches(html,
                    @"(?m)songlist-(?<Id>\d+)[\s\S]+?<a href=""(?<url>/program\?id=(?<PId>\d+)?)"" title="".+?"">(?<name>.+?)</a>");
                foreach (Match match in matches)
                {
                    musicInfos.Add(new MusicInfo
                    {
                        Name = match.Groups["name"].Value,
                        Url = match.Groups["url"].Value,
                        Id = long.Parse(match.Groups["Id"]
                            .Value)
                    });
                }

                return musicInfos;
            }

            return Enumerable.Empty<MusicInfo>();
        }

        public async Task GetDjDetailAsync(long radioId)
        {
            var res = (await RequestAsync(
                "https://music.163.com/api/djradio/v2/get",
                HttpMethod.Post, new
                {
                    id = radioId,
                }, new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi
                }));
            Console.WriteLine(await res.Content.ReadAsStringAsync());

        }
        public async Task<Djradio?> GetDjProgramAsync(int radioId, int limit = 50,
            int offset = 0, bool asc = false)
        {

            // Console.WriteLine(await (await RequestAsync(
            //     "https://music.163.com/weapi/dj/program/byradio",
            //     HttpMethod.Post, new
            //     {
            //         radioId = radioId,
            //         limit = limit,
            //         offset = offset,
            //         asc = asc
            //     }, new NeteaseRequestOption
            //     {
            //         Crypto = CryptoType.Weapi
            //     })).Content.ReadAsStringAsync());
            // ;
            //
            return await (await RequestAsync("https://music.163.com/weapi/dj/program/byradio",
                HttpMethod.Post, new
                {
                    radioId = radioId,
                    limit = limit,
                    offset = offset,
                    asc = asc 
                }, new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi
                })).Content.ReadFromJsonAsync<Djradio>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            // return 0;
        }

        public async Task<List<Djradio>> GetDjProgramToplistAsync(int limit = 50,
            int offset = 0)
        {
            var aa = (
                await (await RequestAsync(
                        "https://music.163.com/api/program/toplist/v1",
                        HttpMethod.Post, new
                        {
                            limit = limit,
                            offset = offset,
                        }, new NeteaseRequestOption
                        {
                            Crypto = CryptoType.Weapi
                        })).Content
                    .ReadFromJsonAsync<List<Djradio>>());
            return aa;
        }
    }
}