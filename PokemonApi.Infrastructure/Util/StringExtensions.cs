using System.Text;
using System.Security.Cryptography;

namespace PokemonApi.Infrastructure.Util
{
    public static class StringExtensions
    {
        public static string GetSHA256(this string str)
        {
            var sha256 = SHA256.Create();
            var encoding = new ASCIIEncoding();
            var sb = new StringBuilder();
            var stream = sha256.ComputeHash(encoding.GetBytes(str));

            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }

            return sb.ToString();
        }
    }
}
