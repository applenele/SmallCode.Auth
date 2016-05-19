using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmallCode.Auth.Helper
{
    public static class StringExtension
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string ToMD5Hash(this string inputValue)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(inputValue));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

    }
}
