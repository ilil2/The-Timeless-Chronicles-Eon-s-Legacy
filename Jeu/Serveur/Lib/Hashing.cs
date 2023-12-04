using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Lib;

public class Hashing
{
    public static string ToSHA256(string s)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

        var sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString("x2"));
        }

        return sb.ToString();
    }
    
    public static byte[] CompressString(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        using (MemoryStream outputStream = new MemoryStream())
        {
            using (GZipStream gzipStream = new GZipStream(outputStream, CompressionMode.Compress, true))
            {
                gzipStream.Write(inputBytes, 0, inputBytes.Length);
            }

            byte[] compressedBytes = outputStream.ToArray();
            return compressedBytes;
        }
    }
    
    public static string DecompressString(byte[] input)
    {
        using (MemoryStream inputStream = new MemoryStream(input))
        using (GZipStream gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
        using (MemoryStream outputStream = new MemoryStream())
        {
            gzipStream.CopyTo(outputStream);
            byte[] decompressedBytes = outputStream.ToArray();
            return Encoding.UTF8.GetString(decompressedBytes);
        }
    }
}