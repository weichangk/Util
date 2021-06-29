namespace Util
{
    /// <summary>
    /// 委托扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 带ref参数的Action
        /// </summary>
        /// <typeparam name="TRef"></typeparam>
        /// <param name="rf"></param>
        public delegate void RefAction<TRef>(ref TRef rf);

        /// <summary>
        /// 带ref参数的Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRef"></typeparam>
        /// <param name="t"></param>
        /// <param name="rf"></param>
        public delegate void RefAction<T, TRef>(T t, ref TRef rf);

        /// <summary>
        /// 带ref参数的Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TRef"></typeparam>
        /// <param name="t"></param>
        /// <param name="t1"></param>
        /// <param name="rf"></param>
        public delegate void RefAction<T, T1, TRef>(T t, T1 t1, ref TRef rf);

        /// <summary>
        /// 带ref参数的Func委托
        /// </summary>
        /// <typeparam name="TRef"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="rf"></param>
        /// <returns></returns>
        public delegate TResult RefFunc<TRef, out TResult>(ref TRef rf);

        /// <summary>
        /// 带ref参数的Func委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TRef"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t"></param>
        /// <param name="rf"></param>
        /// <returns></returns>
        public delegate TResult RefFunc<T, TRef, out TResult>(T t, ref TRef rf);

        /// <summary>
        /// 带ref参数的Func委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TRef"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t"></param>
        /// <param name="t1"></param>
        /// <param name="rf"></param>
        /// <returns></returns>
        public delegate TResult RefFunc<T, T1, TRef, out TResult>(T t, T1 t1, ref TRef rf);

        /// <summary>
        /// 带out参数和返回值的Func委托
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ot"></param>
        /// <returns></returns>
        public delegate TResult OutFunc<TOut, out TResult>(out TOut ot);

        /// <summary>
        /// 带out参数和返回值的Func委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t"></param>
        /// <param name="ot"></param>
        /// <returns></returns>
        public delegate TResult OutFunc<T, TOut, out TResult>(T t, out TOut ot);

        /// <summary>
        /// 带out参数和返回值的Func委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t"></param>
        /// <param name="t1"></param>
        /// <param name="ot"></param>
        /// <returns></returns>
        public delegate TResult OutFunc<T, T1, TOut, out TResult>(T t, T1 t1, out TOut ot);
    }
}
