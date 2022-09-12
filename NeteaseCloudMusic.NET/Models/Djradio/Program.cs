namespace NeteaseCloudMusic.NET.Models.Djradio;


public class Program
{
    public Mainsong MainSong { get; set; }
    public object Songs { get; set; }
    public Dj Dj { get; set; }
    public string BlurCoverUrl { get; set; }
    public Radio Radio { get; set; }
    public long Duration { get; set; }
    public object AuthDto { get; set; }
    public bool Buyed { get; set; }
    public object ProgramDesc { get; set; }
    public object H5Links { get; set; }
    public bool CanReward { get; set; }
    public long AuditStatus { get; set; }
    public object VideoInfo { get; set; }
    public long Score { get; set; }
    public object LiveInfo { get; set; }
    public object Alg { get; set; }
    public object DisPlayStatus { get; set; }
    public long AuditDisPlayStatus { get; set; }
    public object CategoryName { get; set; }
    public object SecondCategoryName { get; set; }
    public bool ExistLyric { get; set; }
    public object DjPlayRecordVo { get; set; }
    public bool Recommended { get; set; }
    public object Icon { get; set; }
    public bool IsPublish { get; set; }
    public object TitbitImages { get; set; }
    public object[] Channels { get; set; }
    public long PubStatus { get; set; }
    public long CategoryId { get; set; }
    public long CreateTime { get; set; }
    public long CreateEventId { get; set; }
    public long ListenerCount { get; set; }
    public long SerialNum { get; set; }
    public long ScheduledPublishTime { get; set; }
    public long CoverId { get; set; }
    public string CoverUrl { get; set; }
    public long SmallLanguageAuditStatus { get; set; }
    public long BdAuditStatus { get; set; }
    public long SecondCategoryId { get; set; }
    public long TrackCount { get; set; }
    public long ProgramFeeType { get; set; }
    public long MalongrackId { get; set; }
    public object Titbits { get; set; }
    public long FeeScope { get; set; }
    public bool Reward { get; set; }
    public long SubscribedCount { get; set; }
    public bool Privacy { get; set; }
    public string CommentThreadId { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public long Id { get; set; }
    public long ShareCount { get; set; }
    public bool Subscribed { get; set; }
    public long LikedCount { get; set; }
    public long CommentCount { get; set; }
}

public class Mainsong
{
    public string Name { get; set; }
    public long Id { get; set; }
    public long Position { get; set; }
    public object[] Alias { get; set; }
    public long Status { get; set; }
    public long Fee { get; set; }
    public long CopyrightId { get; set; }
    public string Disc { get; set; }
    public long No { get; set; }
    public Artist[] Artists { get; set; }
    public Album Album { get; set; }
    public bool Starred { get; set; }
    public float Popularity { get; set; }
    public long Score { get; set; }
    public long StarredNum { get; set; }
    public long Duration { get; set; }
    public long PlayedNum { get; set; }
    public long DayPlays { get; set; }
    public long HearTime { get; set; }
    public string Ringtone { get; set; }
    public object Crbt { get; set; }
    public object Audition { get; set; }
    public string CopyFrom { get; set; }
    public string CommentThreadId { get; set; }
    public object RtUrl { get; set; }
    public long Ftype { get; set; }
    public object[] RtUrls { get; set; }
    public long Copyright { get; set; }
    public object TransName { get; set; }
    public object Sign { get; set; }
    public long Mark { get; set; }
    public object NoCopyrightRcmd { get; set; }
    public Hmusic HMusic { get; set; }
    public object MMusic { get; set; }
    public Lmusic LMusic { get; set; }
    public Bmusic BMusic { get; set; }
    public long Rtype { get; set; }
    public object Rurl { get; set; }
    public long Mvid { get; set; }
    public object Mp3Url { get; set; }
}

public class Album
{
    public string Name { get; set; }
    public long Id { get; set; }
    public object Type { get; set; }
    public long Size { get; set; }
    public long PicId { get; set; }
    public object BlurPicUrl { get; set; }
    public long CompanyId { get; set; }
    public long Pic { get; set; }
    public string PicUrl { get; set; }
    public long PublishTime { get; set; }
    public string Description { get; set; }
    public string Tags { get; set; }
    public object Company { get; set; }
    public string BriefDesc { get; set; }
    public Artist Artist { get; set; }
    public object[] Songs { get; set; }
    public object[] Alias { get; set; }
    public long Status { get; set; }
    public long CopyrightId { get; set; }
    public string CommentThreadId { get; set; }
    public Artist[] Artists { get; set; }
    public object SubType { get; set; }
    public object TransName { get; set; }
    public long Mark { get; set; }
}

public class Artist
{
    public string Name { get; set; }
    public long Id { get; set; }
    public long PicId { get; set; }
    public long Img1V1Id { get; set; }
    public string BriefDesc { get; set; }
    public string PicUrl { get; set; }
    public string Img1V1Url { get; set; }
    public long AlbumSize { get; set; }
    public object[] Alias { get; set; }
    public string Trans { get; set; }
    public long MusicSize { get; set; }
    public long TopicPerson { get; set; }
}

public class Hmusic
{
    public object Name { get; set; }
    public long Id { get; set; }
    public long Size { get; set; }
    public string Extension { get; set; }
    public long Sr { get; set; }
    public long DfsId { get; set; }
    public long Bitrate { get; set; }
    public long PlayTime { get; set; }
    public float VolumeDelta { get; set; }
}

public class Lmusic
{
    public object Name { get; set; }
    public long Id { get; set; }
    public long Size { get; set; }
    public string Extension { get; set; }
    public long Sr { get; set; }
    public long DfsId { get; set; }
    public long Bitrate { get; set; }
    public long PlayTime { get; set; }
    public float VolumeDelta { get; set; }
}

public class Bmusic
{
    public object Name { get; set; }
    public long Id { get; set; }
    public long Size { get; set; }
    public string Extension { get; set; }
    public long Sr { get; set; }
    public long DfsId { get; set; }
    public long Bitrate { get; set; }
    public long PlayTime { get; set; }
    public float VolumeDelta { get; set; }
}

public class Dj
{
    public bool DefaultAvatar { get; set; }
    public long Province { get; set; }
    public long AuthStatus { get; set; }
    public bool Followed { get; set; }
    public string AvatarUrl { get; set; }
    public long AccountStatus { get; set; }
    public long Gender { get; set; }
    public long City { get; set; }
    public long Birthday { get; set; }
    public long UserId { get; set; }
    public long UserType { get; set; }
    public string Nickname { get; set; }
    public string Signature { get; set; }
    public string Description { get; set; }
    public string DetailDescription { get; set; }
    public long AvatarImgId { get; set; }
    public long BackgroundImgId { get; set; }
    public string BackgroundUrl { get; set; }
    public long Authority { get; set; }
    public bool Mutual { get; set; }
    public object ExpertTags { get; set; }
    public object Experts { get; set; }
    public long DjStatus { get; set; }
    public long VipType { get; set; }
    public object RemarkName { get; set; }
    public long AuthenticationTypes { get; set; }
    public object AvatarDetail { get; set; }
    public bool Anchor { get; set; }
    public string AvatarImgIdStr { get; set; }
    public string BackgroundImgIdStr { get; set; }
    public string AvatarImg_IdStr { get; set; }
    public string Brand { get; set; }
}

public class Radio
{
    public object Dj { get; set; }
    public string Category { get; set; }
    public string SecondCategory { get; set; }
    public bool Buyed { get; set; }
    public long Price { get; set; }
    public long OriginalPrice { get; set; }
    public object DiscountPrice { get; set; }
    public long PurchaseCount { get; set; }
    public object LastProgramName { get; set; }
    public object Videos { get; set; }
    public bool Finished { get; set; }
    public bool UnderShelf { get; set; }
    public object LiveInfo { get; set; }
    public long PlayCount { get; set; }
    public bool Privacy { get; set; }
    public object Icon { get; set; }
    public object ManualTagsDto { get; set; }
    public Descpiclist[] DescPicList { get; set; }
    public bool Dynamic { get; set; }
    public object ShortName { get; set; }
    public long CategoryId { get; set; }
    public long TaskId { get; set; }
    public long CreateTime { get; set; }
    public long ProgramCount { get; set; }
    public long PicId { get; set; }
    public long SubCount { get; set; }
    public long LastProgramId { get; set; }
    public long FeeScope { get; set; }
    public long LastProgramCreateTime { get; set; }
    public long RadioFeeType { get; set; }
    public string longervenePicUrl { get; set; }
    public string PicUrl { get; set; }
    public long longervenePicId { get; set; }
    public string Name { get; set; }
    public long Id { get; set; }
    public string Desc { get; set; }
}

public class Descpiclist
{
    public long Type { get; set; }
    public long Id { get; set; }
    public string Content { get; set; }
    public long Height { get; set; }
    public long Width { get; set; }
}
