using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas.core.Commoun
{
    public static class DateTimeExtentions
    {
        public static DateTime GetFirstDayMounth(this DateTime date, int? year = null, int? mouth = null)
        {
            return new(year ?? date.Year, mouth ?? date.Month, day: 1);
        }
        public static DateTime GetLastDayMounth(this DateTime date, int? year = null, int? mouth = null)
        {
            return new DateTime(year ?? date.Year, mouth ?? date.Month, day: 1).AddMonths(1).AddDays(-1);
        }
    }
}