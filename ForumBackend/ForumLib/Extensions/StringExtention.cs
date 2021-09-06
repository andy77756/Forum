using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 將字串轉乘 int，若轉型失敗傳回 null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToInt(this string str)
        {
            return int.TryParse(str, out int number) ? number : (int?)null;
        }
        /// <summary>
        /// 將字串轉乘 double，若轉型失敗傳回 null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double? ToDouble(this string str)
        {
            return double.TryParse(str, out double number) ? number : (double?)null;
        }

    }
}
