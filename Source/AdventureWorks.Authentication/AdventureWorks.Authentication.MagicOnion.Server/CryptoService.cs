using System.Security.Cryptography;
using System.Text;

namespace AdventureWorks.Authentication.MagicOnion.Server;

public static class CryptoService
{
    private static readonly byte[] CryptoKey = Encoding.UTF8.GetBytes(Properties.Resources.CryptoKey);

    public static string Encrypt(string plainText)
    {
        using var aes = GenerateAes();

        var transform = aes.CreateEncryptor(aes.Key, aes.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, transform, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public static string Decrypt(string cipherText)
    {
        using var aes = GenerateAes();

        var transform = aes.CreateDecryptor(aes.Key, aes.IV);

        using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
        using var csDecrypt = new CryptoStream(msDecrypt, transform, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();

    }

    private static Aes GenerateAes()
    {
        var iv = new byte[16];
        var today = BitConverter.GetBytes(DateTime.Today.ToBinary());
        Array.Copy(today, 0, iv, 0, today.Length);
        Array.Copy(today, 0, iv, 8, today.Length);

        var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Key = CryptoKey;
        aes.IV = iv;
        return aes;
    }
}