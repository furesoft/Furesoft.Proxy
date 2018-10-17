using System.Security.Cryptography;
using System.Text;

namespace Furesoft.Proxy.Utils
{
    public static class MD5
    {
        public static string ToHash(string src)
        {
            using (var cs = new MD5CryptoServiceProvider())
            {
                var hashBytes = cs.ComputeHash(Encoding.ASCII.GetBytes(src));

                return Encoding.ASCII.GetString(hashBytes);
            }
        }
    }
}