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
