using fastJSON;
using Newtonsoft.Json;
using System.Configuration;

namespace Util
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public static class JsonHelper
    {
        private static readonly string _josnTool = ConfigurationManager.AppSettings["JsonTool"] ?? "S";

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string jsonData)
        {
            T result;
            switch (_josnTool.ToUpper())
            {
                case "S":
                    result = ServiceStack.Text.StringExtensions.FromJson<T>(jsonData);
                    break;
                case "F":
                    result = JSON.ToObject<T>(jsonData);
                    break;
                case "J":
                    result = JsonConvert.DeserializeObject<T>(jsonData);
                    break;
                default:
                    result = ServiceStack.Text.StringExtensions.FromJson<T>(jsonData);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string SerializeObject(object entity)
        {
            string result;
            switch (_josnTool.ToUpper())
            {
                case "S":
                    result = ServiceStack.Text.StringExtensions.ToJson(entity);
                    break;
                case "F":
                    result = JSON.ToJSON(entity);
                    break;
                case "J":
                    result = JsonConvert.SerializeObject(entity);
                    break;
                default:
                    result = ServiceStack.Text.StringExtensions.ToJson(entity);
                    break;
            }
            return result;
        }
    }
}
