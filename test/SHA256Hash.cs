using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace test
{
    class SHA256Hash
    {
        public static string Hasher(string s)
        {
            string result;
            byte[] temps;
            SHA256 sham = new SHA256Managed();
            temps = sham.ComputeHash(System.Text.Encoding.Unicode.GetBytes(s));
            result = BitConverter.ToString(temps);
            result = result.Replace("-", " ");
            return result;
        }
    }
}
