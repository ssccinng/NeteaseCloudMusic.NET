// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using NeteaseCloudMusic.NET;

NeteasyCloudClient neteasyCloudClient = new();
// await neteasyCloudClient.GetDjProgramToplistAsync(50, 0);
Console.WriteLine((await neteasyCloudClient.GetDjProgramAsync(973537451)).Count);
//  await neteasyCloudClient.GetDjDetailV2Async(973537451);
// Console.WriteLine(await neteasyCloudClient.GetDjBannerAsync());
// Console.WriteLine(await neteasyCloudClient.GetDJCategoryExcludehot());
Console.WriteLine(await neteasyCloudClient.GetMusicCommentAsync(5221167));
// Console.WriteLine(await neteasyCloudClient.GetDjProgramDetailAsync(2511153898));
// ;
//
// await neteasyCloudClient.GetSongDetailAsync(5221167);
// await neteasyCloudClient.GetDjDetailAsync(5221167);
return;
var a = await neteasyCloudClient.GetDjProgramAsync(973537451, offset:0, limit:30);
// var a = await neteasyCloudClient.GetDjradioListAsync(973537451);
foreach (var aProgram in a.Programs)
{
    Console.WriteLine(await neteasyCloudClient.GetSongUrlAsync(aProgram.MainSong.Id));
}
// foreach (var musicInfo in a)
// {
//     Console.WriteLine(musicInfo.Name);
// }