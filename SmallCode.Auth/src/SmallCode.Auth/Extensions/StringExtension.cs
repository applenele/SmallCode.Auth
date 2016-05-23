using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 字符串转Enum
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string inputValue)
        {
            return (T)Enum.Parse(typeof(T), inputValue);
        }
    }
}
