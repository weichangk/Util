using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class RandomHelper
    {
        /// <summary>
        /// 快捷生成的秘钥
        /// </summary>
        public static readonly string PUBLC_KEY = "#$&a21fcc013698b6bd8a0cc120ecf-ABSCN<>RJUOLM@decfb*&";

        /// <summary>
        /// 根据dictionary获取签名
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="key"></param>
        /// <param name="charset"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static string CreateSign(Dictionary<string, string> parameters, string key, bool upper = true)
        {
            /*             
              key拼接在尾部  
             */
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();
            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            sb.Append("key=" + key);
            string sighStr = sb.ToString();
            string sign = EncryptHelper.MD5Encrypt(sighStr);
            if (upper)
            {
                sign = sign.ToUpper();
            }
            else
            {
                sign = sign.ToLower();
            }
            return sign;
        }


        /// <summary>
        /// 时间截，自1970年以来的秒数
        /// </summary>
        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 生成guid随机串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGuidstr(string type = "N")
        {
            return Guid.NewGuid().ToString(type);
        }

    }
}
