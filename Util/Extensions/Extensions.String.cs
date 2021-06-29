using System;
using System.Linq;

namespace Util
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static partial class Extensions
    {
        public static string AMOUNT_FORMAT = "f2";
        /// <summary>
        /// HasValue
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 是否包含元素
        /// </summary>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool Contains(this string[] array, string element, StringComparison comparisonType)
        {
            if (array == null || array.Length == 0) return false;

            return array.Any(f => f.HasValue() && f.Equals(element, comparisonType));
        }

        /// <summary>
        /// ToAmountString
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToAmountString(this decimal s)
        {
            return s.ToString("f2");
        }

        /// <summary>
        /// ToShortAmountString
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCutAmountString(this decimal s)
        {
            if (Math.Floor(s) == s)
                return s.ToString("f0");
            else if (Math.Floor(s * 10) == (s * 10))
                return s.ToString("f1");
            else if (Math.Floor(s * 100) == (s * 100))
                return s.ToString("f2");
            else if (Math.Floor(s * 1000) == (s * 1000))
                return s.ToString("f3");
            return s.ToString("f2");
        }

        /// <summary>
        /// 截取左边指定长度字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string s, int length)
        {
            return !string.IsNullOrEmpty(s) && s.Length > length ? s.Substring(0, length) : s;
        }

        /// <summary>
        /// 截取右边指定长度字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string s, int length)
        {
            return !string.IsNullOrEmpty(s) && s.Length > length ? s.Substring(s.Length - length, length) : s;
        }

        /// <summary>
        /// 获取字符串字节长度(中文2个字符)
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static int ByteLength(this string s)
        {
            if (!s.HasValue()) return 0;
            return System.Text.Encoding.Default.GetByteCount(s);
        }
    }
}
