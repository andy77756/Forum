using System;

namespace ForumLib.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 將字串轉乘 int，若轉型失敗傳回 null
        /// </summary>
        /// <param name="str">string</param>
        /// <returns></returns>
        public static int? ToInt(this string str)
        {
            return int.TryParse(str, out int number) ? number : (int?)null;
        }
        /// <summary>
        /// 將字串轉乘 double，若轉型失敗傳回 null
        /// </summary>
        /// <param name="str">string</param>
        /// <returns></returns>
        public static double? ToDouble(this string str)
        {
            return double.TryParse(str, out double number) ? number : (double?)null;
        }

        /// <summary>
        /// 轉換UnixTime為LocalDatetime
        /// </summary>
        /// <param name="str">string</param>
        /// <returns></returns>
        public static DateTime ToDatetime(this string str)
        {
            var unixTimeStamp = Convert.ToDouble(str);
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
