using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rare_crew_csharp_task.Helper
{
    public class Helper
    {
        public static int CalculateHours(string startDate, string endDate)
        {
            var date1 = DateTime.Parse(startDate);
            var date2 = DateTime.Parse(endDate);

            var diff = (date2 - date1).Hours;

            var duration = DateTime.Parse(endDate).Subtract(DateTime.Parse(startDate));

            return diff;
        }
    }
}