using System.Buffers.Text;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace NeteasyCloudMusic.NET.Utils;

public static class Encrypt
{
     private static string _moudulus =
        "00e0b509f6259df8642dbc35662901477df22677ec152b5ff68ace615bb7b725152b3ab17a876aea8a5aa76d2e417629ec4ee341f56135fccf695280104e0312ecbda92557c93870114af6c9d05c4f7f0c3685b7a46bee255932575cce10b424d813cfe4875d3e82047b97ddef52741d546b8e289dc6935b3ece0462db0a22b8e7";

    private static string _nonce = "0CoJUm6Qyw8W8jud";
    private static string _pubKey = "010001";
    private static Aes _aes = Aes.Create();
    private static RSA _rsa = RSA.Create();

    public static Dictionary<string, string> EncryptedRequest(string text)
    {
        var secKey = CreateSecretKey(16);
        var cc = AesEncrypt(text, _nonce);
        var encText = AesEncrypt(cc, Encoding.UTF8.GetString(secKey));
        var encSecKey = RsaEncrypt(secKey, _pubKey, _moudulus);

        return new Dictionary<string, string> { { "params", encText }, {"encSecKey", encSecKey } };
    }
    public static string AesEncrypt(string text, string seckey)
    {
        int pad = 16 - text.Length % 16;
        char[] padArray = new char[pad];
        for (int i = 0; i < pad; i++)
        {
            padArray[i] = (char)pad;
        }
        _aes.Key = Encoding.UTF8.GetBytes(seckey);
        text = $"{text}{string.Join("", padArray)}";
        var cipText = _aes.EncryptCbc
            (
            Encoding.UTF8.GetBytes(text),
            Encoding.UTF8.GetBytes("0102030405060708")
            );
        //Console.WriteLine("cipText: " + cipText);
        byte[] aa = new byte[cipText.Length + 200];
        Base64.EncodeToUtf8(cipText[..text.Length], aa, out int bytesConsumed, out int bytesWritten);
        //Console.WriteLine("bytesWritten: " + bytesWritten);
        //Console.WriteLine(Encoding.UTF8.GetString(aa, 0,bytesWritten));
        return Encoding.UTF8.GetString(aa, 0, bytesWritten);
    }

    public static string RsaEncrypt(Byte[] text, string pubKey, string moudulus)
    {
        text = text.Reverse().ToArray();
        var gg = Encoding.UTF8.GetString(text);
        var hext = BitConverter.ToString(text);
        var pubbig = int.Parse(pubKey, NumberStyles.HexNumber);
        var moudulusbig = BigInteger.Parse(moudulus, NumberStyles.HexNumber);
        var rs = PowerBySquaringDS(BigInteger.Parse(ConvertToHex(gg), NumberStyles.HexNumber),
            pubbig, 
            moudulusbig);
        var rss = rs.ToString("x256")[^256..];
        return rss;
    }

    public static byte[] CreateSecretKey(int size)
    {
        Byte[] randStr = new Byte[16];
         Random.Shared.NextBytes(randStr);
         Console.WriteLine(Convert.ToHexString(randStr));
         return Encoding.UTF8.GetBytes(Convert.ToHexString(randStr).ToLower())[..16];
         return randStr;
    }
    private static string ConvertToHex(string asciiString)
    {
        string hex = "";
        foreach (char c in asciiString)
        {
            int tmp = c;
            hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
        }
        return hex;
    }
    
    private static string Hex2Binary1(string hexvalue)
    {
        StringBuilder binaryval = new StringBuilder();
        for(int i=0; i < hexvalue.Length; i++)
        {
            string byteString = hexvalue.Substring(i, 1);
            byte b = Convert.ToByte(byteString, 16);
            binaryval.Append(Convert.ToString(b, 2).PadLeft(4, '0'));
        }
        return binaryval.ToString();
    }

    
    public static long PowerBySquaring(long baseNumber, int exponent, int mod)
    {
        long result = 1;
        while (exponent != 0)
        {
            if ((exponent & 1) == 1)
            {
                result *= baseNumber;
                result %= mod;
            }
            exponent >>= 1;
            baseNumber *= baseNumber;
            baseNumber %= mod;
        }

        return result;
    }
    
    public static BigInteger PowerBySquaringDS(BigInteger baseNumber, int exponent, BigInteger mod)
    {
        BigInteger result = 1;
        while (exponent != 0)
        {
            if ((exponent & 1) == 1)
            {
                result *= baseNumber;
                result %= mod;
            }
            exponent >>= 1;
            baseNumber *= baseNumber;
            baseNumber %= mod;
        }

        return result;
    }
}