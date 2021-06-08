using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Util
{
    public sealed class EncryptHelper
    {
        #region Base64加密解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }
        #endregion

        #region DES加密解密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <param name="key">8位字符的密钥字符串</param>
        /// <param name="iv">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        public static string DESEncrypt(string data, string key = "wsx456pp", string iv = "asc45gc7")
        {
            byte[] byKey = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] byIV = System.Text.Encoding.UTF8.GetBytes(iv);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">8位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        public static string DESDecrypt(string data, string key = "wsx456pp", string iv = "asc45gc7")
        {
            byte[] byKey = Encoding.UTF8.GetBytes(key);
            byte[] byIV = Encoding.UTF8.GetBytes(iv);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
        #endregion

        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string input)
        {
            return MD5Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string MD5Encrypt(string input, Encoding encode)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(encode.GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            return sb.ToString();
        }

        /// <summary>
        /// MD5对文件流加密
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static string MD5Encrypt(Stream stream)
        {
            MD5 md5serv = MD5CryptoServiceProvider.Create();
            byte[] buffer = md5serv.ComputeHash(stream);
            StringBuilder sb = new StringBuilder();
            foreach (byte var in buffer)
                sb.Append(var.ToString("x2"));
            return sb.ToString();
        }

        /// <summary>
        /// MD5加密(返回16位加密串)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string input, Encoding encode)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string result = BitConverter.ToString(md5.ComputeHash(encode.GetBytes(input)), 4, 8);
            result = result.Replace("-", "");
            return result;
        }
        #endregion

        #region 3DES 加密解密
        /// <summary>
        /// 3DES 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DES3Encrypt(string data, string key = "wefsdq7%(sx456pp")
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = Encoding.UTF8.GetBytes(key);
            DES.Mode = CipherMode.CBC;
            DES.Padding = PaddingMode.PKCS7;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DES3Decrypt(string data, string key = "wefsdq7%(sx456pp")
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = Encoding.UTF8.GetBytes(key);
            DES.Mode = CipherMode.CBC;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(data);
                result = Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        #endregion

        //#region RSA 加解密
        ///// <summary>
        ///// RSA 加密
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static string RSAEncrypt(string data)
        //{
        //    return SissCloud.Encrypt.RsaHelper.EncryptRegisterString(data);
        //}

        ///// <summary>
        ///// RSA 解密
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static string RSADecrypt(string data)
        //{
        //    return SissCloud.Encrypt.RsaHelper.DecryptRegisterString(data);
        //}

        //#endregion

        #region TripleDESCryptoServiceProvider
        //12个字符  
        const string _customIV = "8vZKRj5yfzU=";
        //32个字符  
        const string _customKey = "l1is9ooXLf772Z+Aht9DdpQGoa3+SA7f";

        /// <summary>  
        /// 加密字符串  
        /// </summary>  
        /// <param name="data"></param>  
        /// <returns></returns>  
        public static string TripleDESEncrypt(string strData)
        {
            try
            {
                string encryptPassword = string.Empty;

                SymmetricAlgorithm algorithm = new TripleDESCryptoServiceProvider();
                algorithm.Key = Convert.FromBase64String(_customKey);
                algorithm.IV = Convert.FromBase64String(_customIV);
                algorithm.Mode = CipherMode.ECB;
                algorithm.Padding = PaddingMode.PKCS7;

                ICryptoTransform transform = algorithm.CreateEncryptor();

                byte[] data = (new System.Text.ASCIIEncoding()).GetBytes(strData);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);

                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                encryptPassword = Convert.ToBase64String(memoryStream.ToArray());

                memoryStream.Close();
                cryptoStream.Close();

                return encryptPassword;
            }
            catch { }

            return "";
        }

        /// <summary>  
        /// 解密字符串  
        /// </summary>  
        /// <param name="password"></param>  
        /// <returns></returns>  
        public static string TripleDESDecrypt(string data)
        {
            try
            {
                string decryptPassword = string.Empty;

                SymmetricAlgorithm algorithm = new TripleDESCryptoServiceProvider();
                algorithm.Key = Convert.FromBase64String(_customKey);
                algorithm.IV = Convert.FromBase64String(_customIV);
                algorithm.Mode = CipherMode.ECB;
                algorithm.Padding = PaddingMode.PKCS7;

                ICryptoTransform transform = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

                byte[] buffer = Convert.FromBase64String(data);
                MemoryStream memoryStream = new MemoryStream(buffer);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream, System.Text.Encoding.ASCII);
                decryptPassword = reader.ReadToEnd();

                reader.Close();
                cryptoStream.Close();
                memoryStream.Close();

                return decryptPassword;
            }
            catch { }

            return "";
        }
        #endregion
    }
}
