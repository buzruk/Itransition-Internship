using System.Text;
using Org.BouncyCastle.Crypto.Digests;

public class Program
{
  private const string Email = "buzurgmexrs@gmail.com";
  private static readonly DirectoryInfo _directory = new(@"Z:\Users\buzurgmexr\Desktop\Itransition-Internship\Task2\task2");
  private static readonly Sha3Digest _hashAlgorithm = new(256);

  static void Main(string[] args)
  {
    var files = _directory.GetFiles();
    var hashes = new List<string>();

    for (int i = 0; i < files.Length; i++)
    {
      var fileData = GetFileData(files[i]);

      string hashString = GetHashString(fileData);

      hashes.Add(hashString);

      Console.WriteLine($"#{i + 1} '{files[i].Name}' SizeOK: {files[i].Length == fileData.Length}  Result: {hashString}");
    }

    hashes.Sort();
    hashes.Add(Email);

    var hashesString = string.Join("", hashes);

    Console.WriteLine($"Result: {GetHashString(Encoding.ASCII.GetBytes(hashesString))}");
  }

  private static byte[] GetFileData(FileInfo fileInfo)
  {
    using var fileStream = fileInfo.Open(FileMode.Open);
    var bytesToRead = fileStream.Length;
    var result = new byte[bytesToRead];

    int numBytesRead = 0;

    while (bytesToRead > 0)
    {
      int n = fileStream.Read(result, numBytesRead, (int)bytesToRead);

      if (n == 0)
        break;

      numBytesRead += n;
      bytesToRead -= n;
    }

    return result;
  }

  private static string GetHashString(byte[] data)
  {
    byte[] hash = new byte[32];

    _hashAlgorithm.BlockUpdate(data, 0, data.Length);
    _hashAlgorithm.DoFinal(hash, 0);

    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
  }
}
