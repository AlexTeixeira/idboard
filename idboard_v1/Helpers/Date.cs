using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idboard_v1.Helpers
{
    public class Date
    {
        public static void GetWeek(DateTime now, CultureInfo cultureInfo, out DateTime begining, out DateTime end, String action = "Now")
        {
            if (now == null)
                throw new ArgumentNullException("now");
            if (cultureInfo == null)
                throw new ArgumentNullException("cultureInfo");

            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            switch (action)
            {
                case "Next":
                    now = now.AddDays(7);
                    break;
                case "Previous":
                    now = now.AddDays(-6);
                    break;
                default:
                    break;
            }

            int offset = firstDayOfWeek - now.DayOfWeek;
            if (offset != 1)
            {
                DateTime weekStart = now.AddDays(offset);
                DateTime endOfWeek = weekStart.AddDays(6);
                begining = weekStart;
                end = endOfWeek;
            }
            else
            {
                begining = now.AddDays(-6);
                end = now;
            }
        }

    }
}
