using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parosan.Service
{
    class HasingService
    {

        public string sha256Hash(string data)
        {

            //create sha256 object
            SHA256 sha256Hash = SHA256.Create();

            //get bytes text and hash bytes
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

            //create strig builder
            StringBuilder builder = new StringBuilder();

            //append hashbeytes to string builder
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();

        }

        public string md5hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(Encoding.UTF8.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //her baytı 2 hexadecimal hane olarak değiştirir
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

    }
}
