using System.Text.RegularExpressions;

namespace Util
{
    public class RegexHelper
    {
        public static bool IsNumber(string input)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhone(string input)
        {
            Regex regex = new Regex(@"^1([38][0-9]|4[579]|5[0-3,5-9]|6[6]|7[0135678]|9[89])\d{8}$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 正数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPositiveAmount(string input)
        {
            Regex regex = new Regex(@"^(0|[1-9][0-9]*)(\.\d+)?$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 浮点数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDouble(string input)
        {
            Regex regex = new Regex(@"^-?(0|[0-9][0-9]*)(\.\d+)?$");
            return regex.IsMatch(input);
        }
    }
}
