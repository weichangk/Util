using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    /// <summary>
    /// 异常信息扩展
    /// </summary>
    [Serializable]
    public class ExceptionEx : Exception
    {
        /*
            try
            {
                //
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
         */

        /// <summary>
        ///使用异常消息与一个内部异常实例化一个 类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在DalException内部的异常实例</param>
        public ExceptionEx(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        ///向调用层抛出业务逻辑访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static ExceptionEx ThrowBusinessException(Exception e, string msg = "业务逻辑层异常，详情请查看日志信息。")
        {
            return new ExceptionEx(msg, e);
        }
        /// <summary>
        /// 向调用层抛出数据服务访问层异常
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ExceptionEx ThrowServiceException(Exception e, string msg = "数据服务层异常，详情请查看日志信息。")
        {
            return new ExceptionEx(msg, e);
        }
        /// <summary>
        ///向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static ExceptionEx ThrowDataAccessException(Exception e, string msg = "数据访问层异常，详情请查看日志信息。")
        {
            return new ExceptionEx(msg, e);
        }
        /// <summary>
        ///     向调用层抛出组件异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static ExceptionEx ThrowComponentException(Exception e, string msg = "组件异常，详情请查看日志信息。")
        {
            return new ExceptionEx(msg, e);
        }
    }
}
