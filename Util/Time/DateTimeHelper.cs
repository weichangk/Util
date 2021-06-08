using System;

namespace Util
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 获取某天开始时间
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static DateTime GetDayStart(DateTime dt)
        {
            return dt.Date;
        }

        /// <summary>
        /// 获取某天结束时间
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static DateTime GetDayEnd(DateTime dt)
        {
            return dt.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取本月开始时间
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static DateTime GetMonthStart(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        /// <summary>
        /// 获取本月月末时间
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static DateTime GetMonthEnd(DateTime dt)
        {
            DateTime nextFirst = GetMonthStart(dt.AddMonths(1));
            return new DateTime(dt.Year, dt.Month, nextFirst.AddDays(-1).Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取日期所属年份开始时间
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static DateTime GetYearStart(DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }

        /// <summary>
        /// 获取日期所属年份截止时间
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static DateTime GetYearEnd(DateTime dt)
        {
            DateTime nextFirst = GetYearStart(dt.AddYears(1));
            return nextFirst.AddMilliseconds(-1);
        }

        /// <summary>
        /// 获取周开始时间
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static DateTime GetWeekStart(DateTime dt)
        {
            var weekIndex = (byte)dt.DayOfWeek;
            return dt.AddDays(-weekIndex);
        }

        /// <summary>
        /// 获取周结束时间
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static DateTime GetWeekEnd(DateTime dt)
        {
            var weekIndex = (byte)dt.DayOfWeek;
            return dt.AddDays(6 - weekIndex);
        }

        /// <summary>
        /// 获取指定日期的星期几标记
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetWeekDayName(DateTime date)
        {
            string name = "";
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    name = "星期日";
                    break;
                case DayOfWeek.Monday:
                    name = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    name = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    name = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    name = "星期四";
                    break;
                case DayOfWeek.Friday:
                    name = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    name = "星期六";
                    break;
                default:
                    break;
            }

            return name;
        }

        /// <summary>
        /// 返回时间相对于今天的星期几名称（上周之内）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetWeekDayNameRelativeToToday(DateTime date)
        {
            string name = "";
            int todayIndex = (int)DateTime.Today.DayOfWeek;
            bool isLastWeek = DateTime.Today.AddDays(-todayIndex) > date && date < DateTime.Today && date >= DateTime.Today.AddDays(-7 - todayIndex);
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    name = "日";
                    break;
                case DayOfWeek.Monday:
                    name = "一";
                    break;
                case DayOfWeek.Tuesday:
                    name = "二";
                    break;
                case DayOfWeek.Wednesday:
                    name = "三";
                    break;
                case DayOfWeek.Thursday:
                    name = "四";
                    break;
                case DayOfWeek.Friday:
                    name = "五";
                    break;
                case DayOfWeek.Saturday:
                    name = "六";
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(name)) name = (isLastWeek ? "上周" : "星期") + name;

            return name;
        }

        /// <summary>
        /// 返回两个时间的相对天数时间差
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());
            TimeSpan sp = end.Subtract(start);
            return sp.Days;
        }
    }
}
