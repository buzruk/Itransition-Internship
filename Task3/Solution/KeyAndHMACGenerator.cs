using System;
using System.Security.Cryptography;
using System.Text;

namespace Task3;

class KeyAndHMACGenerator
{
  public static string KeyGenerator()
  {
    RandomNumberGenerator random = RandomNumberGenerator.Create();

    byte[] bytes = new byte[32];

    random.GetNonZeroBytes(bytes);

    string result = Concatinate(bytes);
    
    return result;
  }

  public static string HMACGenerator(string sKey, string sMessage)
  {
    byte[] keyByte = new ASCIIEncoding().GetBytes(sKey);

    byte[] messageBytes = new ASCIIEncoding().GetBytes(sMessage);

    byte[] hashmessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);

    string result = Concatinate(hashmessage);

    return result;
  }

  private static string Concatinate(byte[] hashmessage)
    => String.Concat(Array.ConvertAll(hashmessage, x => x.ToString("x2")));
}
