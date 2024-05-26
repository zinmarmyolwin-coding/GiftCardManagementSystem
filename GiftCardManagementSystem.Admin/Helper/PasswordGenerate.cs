using System.Security.Cryptography;
using System.Text;

namespace GiftCardManagementSystem.Admin.Helper
{
    public static class PasswordGenerate
    {
        public static string SHA256HexHashString(this string password, string userName)
        {
            password = password.Trim();
            userName = userName.Trim();

            string saltedCode = EncodedbySalted(userName);//username
            string hashString = string.Empty;
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.Default.GetBytes(password + saltedCode));
                hashString = ToHex(hash, false);
            }

            return hashString;
        }

        public static string EncodedbySalted(string decodestring)
        {
            decodestring = decodestring.ToLower()
                .Replace("a", "@")
                .Replace("i", "!")
                .Replace("l", "1")
                .Replace("e", "3")
                .Replace("o", "0")
                .Replace("s", "$")
                .Replace("n", "&");
            return decodestring;
        }

        public static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            return result.ToString();
        }
    }
}
