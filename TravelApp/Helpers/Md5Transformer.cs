using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Helpers
{
    public class Md5Transformer
    {
        public static string GetMd5String(string password){
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] md5dataBytes = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Encoding.UTF8.GetString(md5dataBytes);
        }
    }
}
