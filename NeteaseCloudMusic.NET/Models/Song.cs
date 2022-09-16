namespace NeteaseCloudMusic.NET.Models;

public class Song
{
    public Datum[] Data { get; set; }
    public int Code { get; set; }
}

public class Datum
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int Br { get; set; }
    public int Size { get; set; }
    public string Md5 { get; set; }
    public int Code { get; set; }
    public int Expi { get; set; }
    public string Type { get; set; }
    public float Gain { get; set; }
    public int Fee { get; set; }
    public object Uf { get; set; }
    public int Payed { get; set; }
    public int Flag { get; set; }
    public bool CanExtend { get; set; }
    public object FreeTrialInfo { get; set; }
    public object Level { get; set; }
    public object EncodeType { get; set; }
    public object FreeTrialPrivilege { get; set; }
    public Freetimetrialprivilege FreeTimeTrialPrivilege { get; set; }
    public int UrlSource { get; set; }
    public int RightSource { get; set; }
    public string PodcastCtrp { get; set; }
    public object EffectTypes { get; set; }
    public int Time { get; set; }
}

public class Freetimetrialprivilege
{
    public bool ResConsumable { get; set; }
    public bool UserConsumable { get; set; }
    public int Type { get; set; }
    public int RemainTime { get; set; }
}



public class Rootobject
{
    public SongDetail[] Songs { get; set; }
    public Privilege[] Privileges { get; set; }
    public int Code { get; set; }
}

public class SongDetail
{
    public string Name { get; set; }
    public int Id { get; set; }
    public int Pst { get; set; }
    public int T { get; set; }
    public Ar[] Ar { get; set; }
    public object[] Alia { get; set; }
    public float Pop { get; set; }
    public int St { get; set; }
    public string Rt { get; set; }
    public int Fee { get; set; }
    public int V { get; set; }
    public object Crbt { get; set; }
    public string Cf { get; set; }
    public Al Al { get; set; }
    public int Dt { get; set; }
    public H H { get; set; }
    public M M { get; set; }
    public L L { get; set; }
    public object Sq { get; set; }
    public object Hr { get; set; }
    public object A { get; set; }
    public string Cd { get; set; }
    public int No { get; set; }
    public object RtUrl { get; set; }
    public int Ftype { get; set; }
    public object[] RtUrls { get; set; }
    public int DjId { get; set; }
    public int Copyright { get; set; }
    public int SId { get; set; }
    public int Mark { get; set; }
    public int OriginCoverType { get; set; }
    public object OriginSongSimpleData { get; set; }
    public object TagPicList { get; set; }
    public bool ResourceState { get; set; }
    public int Version { get; set; }
    public object SongJumpInfo { get; set; }
    public object EntertainmentTags { get; set; }
    public object AwardTags { get; set; }
    public int Single { get; set; }
    public object NoCopyrightRcmd { get; set; }
    public int Rtype { get; set; }
    public object Rurl { get; set; }
    public int Mst { get; set; }
    public int Cp { get; set; }
    public int Mv { get; set; }
    public long PublishTime { get; set; }
}

public class Al
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PicUrl { get; set; }
    public object[] Tns { get; set; }
    public long Pic { get; set; }
}

public class H
{
    public int Br { get; set; }
    public int Fid { get; set; }
    public int Size { get; set; }
    public float Vd { get; set; }
    public int Sr { get; set; }
}

public class M
{
    public int Br { get; set; }
    public int Fid { get; set; }
    public int Size { get; set; }
    public float Vd { get; set; }
    public int Sr { get; set; }
}

public class L
{
    public int Br { get; set; }
    public int Fid { get; set; }
    public int Size { get; set; }
    public float Vd { get; set; }
    public int Sr { get; set; }
}

public class Ar
{
    public int Id { get; set; }
    public string Name { get; set; }
    public object[] Tns { get; set; }
    public object[] Alias { get; set; }
}

public class Privilege
{
    public int Id { get; set; }
    public int Fee { get; set; }
    public int Payed { get; set; }
    public int St { get; set; }
    public int Pl { get; set; }
    public int Dl { get; set; }
    public int Sp { get; set; }
    public int Cp { get; set; }
    public int Subp { get; set; }
    public bool Cs { get; set; }
    public int Maxbr { get; set; }
    public int Fl { get; set; }
    public bool Toast { get; set; }
    public int Flag { get; set; }
    public bool PreSell { get; set; }
    public int PlayMaxbr { get; set; }
    public int DownloadMaxbr { get; set; }
    public string MaxBrLevel { get; set; }
    public string PlayMaxBrLevel { get; set; }
    public string DownloadMaxBrLevel { get; set; }
    public string PlLevel { get; set; }
    public string DlLevel { get; set; }
    public string FlLevel { get; set; }
    public object Rscl { get; set; }
    public Freetrialprivilege FreeTrialPrivilege { get; set; }
    public Chargeinfolist[] ChargeInfoList { get; set; }
}

public class Freetrialprivilege
{
    public bool ResConsumable { get; set; }
    public bool UserConsumable { get; set; }
    public object ListenType { get; set; }
}

public class Chargeinfolist
{
    public int Rate { get; set; }
    public object ChargeUrl { get; set; }
    public object ChargeMessage { get; set; }
    public int ChargeType { get; set; }
}

