using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Util
{
    public static class TaskHelper
    {
        /// <summary>
        /// 开启任务
        /// </summary>
        /// <param name="taskFactory"></param>
        /// <param name="method">System.Reflection.MethodBase.GetCurrentMethod()</param>
        /// <param name="action"></param>
        /// <param name="exAction"></param>
        /// <returns>返回带有任务名称标记的任务，标记于AsyncState上面</returns>
        public static Task<object> StartNewTask(this TaskFactory taskFactory, MethodBase method, Action action, Action<Exception> exAction)
        {
            string taskName = "ClientFunc.StartNewTask";
            try
            {
                if (method != null) taskName = $"{method.DeclaringType.Name}.{method.Name}";
            }
            catch { }
            return Task.Factory.StartNew((tsName) =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    exAction?.Invoke(ex);
                }

                return (object)taskName;
            }, taskName);
        }

        /// <summary>
        /// 执行winform界面异步
        /// </summary>
        /// <param name="taskAction"></param>
        /// <param name="endTaskAction"></param>
        /// <param name="control"></param>
        public static void StartAsync(Action taskAction, Action<Exception> exTaskAction, Action endTaskAction, Action<Exception> exEndTaskAction, Control control)
        {
            if (control == null) return;

            Task.Factory.StartNew(() =>
            {
                try
                {
                    taskAction();
                }
                catch (Exception ex)
                {
                    exTaskAction?.Invoke(ex);
                }
                finally
                {
                    if (endTaskAction != null)
                    {
                        try
                        {
                            control.Invoke(endTaskAction);
                        }
                        catch (Exception ex)
                        {
                            exEndTaskAction?.Invoke(ex);
                        }
                    }
                }

            });
        }
    }
}
