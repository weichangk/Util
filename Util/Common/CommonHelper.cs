using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Util
{
    /// <summary>
    /// 常用公共类
    /// </summary>
    public static class CommonHelper
    {
        #region Stopwatch计时器
        /// <summary>
        /// 计时器开始
        /// </summary>
        /// <returns></returns>
        public static Stopwatch TimerStart()
        {
            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            return watch;
        }
        /// <summary>
        /// 计时器结束
        /// </summary>
        /// <param name="watch">Stopwatch</param>
        /// <returns></returns>
        public static string TimerEnd(Stopwatch watch)
        {
            watch.Stop();
            double costtime = watch.ElapsedMilliseconds;
            return costtime.ToString();
        }
        #endregion

        #region 删除数组中的重复项
        /// <summary>
        /// 删除数组中的重复项
        /// </summary>
        /// <param name="values">重复值</param>
        /// <returns></returns>
        public static string[] RemoveDup(string[] values)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < values.Length; i++)//遍历数组成员
            {
                if (!list.Contains(values[i]))
                {
                    list.Add(values[i]);
                };
            }
            return list.ToArray();
        }
        #endregion

        #region 自动生成日期编号
        /// <summary>
        /// 自动生成编号  201008251145409865
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            string code = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom;//形如
            return code;
        }
        #endregion

        #region 生成0-9随机数
        /// <summary>
        /// 生成0-9随机数
        /// </summary>
        /// <param name="codeNum">生成长度</param>
        /// <returns></returns>
        public static string RndNum(int codeNum)
        {
            StringBuilder sb = new StringBuilder(codeNum);
            Random rand = new Random();
            for (int i = 1; i < codeNum + 1; i++)
            {
                int t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();

        }
        #endregion

        #region 删除最后一个字符之后的字符
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        /// <param name="str">字串</param>
        /// <returns></returns>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        /// <param name="str">字串</param>
        /// <param name="strchar">指定的字符</param>
        /// <returns></returns>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }
        /// <summary>
        /// 删除最后结尾的长度
        /// </summary>
        /// <param name="str">字串</param>
        /// <param name="Length">删除长度</param>
        /// <returns></returns>
        public static string DelLastLength(string str, int Length)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            str = str.Substring(0, str.Length - Length);
            return str;
        }
        #endregion

        #region 版本号
        /// <summary>
        /// 获取本地版本
        /// </summary>
        /// <returns></returns>
        public static string GetLocalVersion(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            System.Diagnostics.FileVersionInfo mfv = System.Diagnostics.FileVersionInfo.GetVersionInfo(filePath);
            return mfv.Comments;
        }

        /// <summary>
        /// 获取本地版本
        /// </summary>
        /// <returns></returns>
        public static string GetLocalVersion()
        {
            System.Reflection.Assembly ma = System.Reflection.Assembly.GetEntryAssembly();
            return GetLocalVersion(ma.Location);
        }

        /// <summary>
        /// 版本号检测
        /// </summary>
        /// <param name="severV"></param>
        /// <param name="localV"></param>
        /// <returns></returns>
        public static bool CheckVersion(string versionIndex, string severV, string localV)
        {
            severV = severV.Replace(versionIndex, string.Empty).Replace(" ", string.Empty).Trim();
            localV = localV.Replace(versionIndex, string.Empty).Replace(" ", string.Empty).Trim();
            if (string.IsNullOrEmpty(severV))// || string.IsNullOrEmpty(localV))
                return false;

            if (CommonHelper.TryInt(severV, 0) > CommonHelper.TryInt(localV, 0))
                return true;

            return false;
        }
        private static int TryInt(object obj, int defInt)
        {
            int inttemp = 0;
            if (obj == null || obj == DBNull.Value || obj.Equals(string.Empty))
                return defInt;
            obj = obj.ToString().Replace("￥", "").Replace("$", "");
            if (obj.ToString().Contains("."))
            {
                obj = float.Parse(obj.ToString());
            }
            if (Int32.TryParse(obj.ToString(), out inttemp))
                return inttemp;
            else
                return defInt;
        }
        #endregion
    }
}
