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

        /// <summary>
        /// 通过html获取电台音乐信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取电台信息V2Api
        /// </summary>
        /// <param name="radioId">电台id</param>
        public async Task GetDjDetailV2Async(long radioId)
        {
            var res = (await RequestAsync(
                "https://music.163.com/weapi/djradio/v2/get",
                //"https://music.163.com/weapi/djradio/get",
                HttpMethod.Post, new
                {
                    id = radioId,
                }, new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi
                }));
            Console.WriteLine(await res.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// 获取电台信息V1Api
        /// </summary>
        /// <param name="radioId">电台id</param>
        public async Task GetDjDetailV1Async(long radioId)
        {
            var res = (await RequestAsync(
                //"https://music.163.com/api/djradio/v2/get",
                "https://music.163.com/weapi/djradio/get",
                HttpMethod.Post, new
                {
                    id = radioId,
                }, new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi
                }));
            Console.WriteLine(await res.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// 获取电台节目列表
        /// </summary>
        /// <param name="radioId">电台id</param>
        /// <param name="limit">个数限制</param>
        /// <param name="offset">偏移</param>
        /// <param name="asc">排序顺序</param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取新晋电台榜/热门电台榜
        /// </summary>
        /// <param name="limit">个数限制</param>
        /// <param name="offset">偏移</param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取热门电台
        /// </summary>
        /// <param name="limit">个数限制</param>
        /// <param name="offset">偏移</param>
        public async Task GetDjHotAsync(int limit = 50,
            int offset = 0)
        {
            var res = await RequestAsync(
                "https://music.163.com/weapi/djradio/hot/v1",
                HttpMethod.Post, new
                {
                    limit = limit,
                    offset = offset,
                }, new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi
                });

            Console.WriteLine(await res.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// 获取电台节目详情
        /// </summary>
        /// <param name="id">电台节目id</param>
        /// <returns></returns>
        public async Task<object> GetDjProgramDetailAsync(long id)
        {
            var res = await RequestAsync(
                "https://music.163.com/weapi/dj/program/detail",
                HttpMethod.Post, new
                {
                    id = id,
                }, new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi
                });
            return await res.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 获取电台banner
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetDjBannerAsync()
        {
            var res = await RequestAsync(
                "https://music.163.com/weapi/djradio/banner/get",
                HttpMethod.Post, new
                {
                }, new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi,
                    Cookie = new Dictionary<string, string> { { "os", "pc" } }
                });
            return await res.Content.ReadAsStringAsync();
        }
/// <summary>
/// 获取电台非热门类型
/// </summary>
/// <returns></returns>
        public async Task<object> GetDJCategoryExcludehot()
        {
            var res = await RequestAsync(
                "https://music.163.com/weapi/djradio/category/excludehot",
                HttpMethod.Post, new
                {
                }, new NeteaseRequestOption
                {
                    Crypto = CryptoType.Weapi,
                });
            return await res.Content.ReadAsStringAsync();
        }
    }
}