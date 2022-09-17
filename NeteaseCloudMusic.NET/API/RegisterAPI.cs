using NeteaseCloudMusic.NET.Models;

namespace NeteaseCloudMusic.NET;

public partial class NeteasyCloudClient
{
    public async Task RegisterAnonimous()
    {
       var res = await RequestAsync("https://music.163.com/weapi/register/anonimous", 
            HttpMethod.Post, 
            new
            {
                username = "MzEwMjcwYmY0Y2Y0ODcwMzU0ZDFkZmIxMmMzMGYyMTkgVlBaanMwNmtrb1BYMGxOVzVUMUJ3Zz09",
                // csrf_token = ""
            }, 
            new NeteaseRequestOption
            {
                Crypto = CryptoType.Weapi
            });
       Console.WriteLine(await res.Content.ReadAsStringAsync());
       Console.WriteLine( res.Headers.GetValues("set-cookie"));
    }
}