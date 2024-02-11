using rare_crew_csharp_task.Utilities.ChartUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace rare_crew_csharp_task.Models
{
    public class EmployeeTotalHoursChartData
    {
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<EmployeeViewModel> EmployeeData { get; set; }

        public MemoryStream ChartImageStream()
        {
            var chart = new EmployeeWorkHoursChart(this);
            return chart.GetChartImage(Width, Height);
        }

        public string ChartImageMap(string name)
        {
            var chart = new EmployeeWorkHoursChart(this);
            return chart.GetChartImageMap(Width, Height, name);
        }
    }
}