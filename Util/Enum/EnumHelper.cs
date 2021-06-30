using System;
using System.ComponentModel;
using System.Linq;

namespace Util
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举Char值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char EnumCharValue<T>(this T value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return Convert.ToChar(value);
        }

        /// <summary>
        /// 获取Char枚举的String值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EnumCharStr<T>(this T value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return Convert.ToChar(value).ToString();
        }

        /// <summary>
        /// 获取枚举Int值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int EnumIntValue<T>(this T value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// 获取枚举short值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short EnumShortValue<T>(this T value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return Convert.ToInt16(value);
        }

        /// <summary>
        /// 获取枚举byte值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte EnumByteValue<T>(this T value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return Convert.ToByte(value);
        }

        /// <summary>
        /// 获取枚举Int值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EnumIntStr<T>(this T value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return Convert.ToInt32(value).ToString();
        }

        /// <summary>
        /// 根据枚举值获取枚举的描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EnumDescription<T>(this T value) where T : struct, IComparable, IConvertible, IFormattable
        {
            var enumType = typeof(T);
            var field = typeof(T).GetField(Enum.GetName(enumType, value));

            var attr = field != null ? field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute : null;

            return attr != null ? attr.Description : "";
        }

        /// <summary>
        /// 获取枚举所有"或"运算的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int EnumUnionResult(Type enumType)
        {
            int result = 0;
            foreach (var val in Enum.GetValues(enumType))
            {
                result = result | (int)val;
            }

            return result;
        }
    }
}
