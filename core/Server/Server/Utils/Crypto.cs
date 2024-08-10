using System.Security.Cryptography;
using System.Text;

public class Crypto
{
    public static string Hash(string value)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashBytes = sha256.ComputeHash(valueBytes);

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}