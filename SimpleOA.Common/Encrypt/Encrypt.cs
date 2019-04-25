using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Common
{
    public class Encrypt
    {
        public static string Md5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5Buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in md5Buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
