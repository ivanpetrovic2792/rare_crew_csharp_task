using rare_crew_csharp_task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace rare_crew_csharp_task.Utilities.ChartUtilities
{
    public class EmployeeWorkHoursChart : PieChartBase
    {
        private EmployeeTotalHoursChartData chartData;

        public EmployeeWorkHoursChart(EmployeeTotalHoursChartData chartData)
        {
            this.chartData = chartData;
        }

        protected override void AddChartTitle()
        {
            ChartTitle = chartData.Title;
        }

        protected override void AddChartSeries()
        {
            ChartSeriesData = new List<Series>();
            var series = new Series()
            {
                ChartType = SeriesChartType.Pie,
                BorderWidth = 1
            };

            var workingHours = chartData.EmployeeData;

            var sumOfHours = workingHours.Select(x => x.TotalTimeInHrs).Sum();

            foreach (var hours in workingHours)
            {
                var point = new DataPoint();
                point.IsValueShownAsLabel = true;
                point.AxisLabel = hours.EmployeeName;
                point.ToolTip = hours.EmployeeName + " " +
                      hours.TotalTimeInHrs.ToString("#0.###");
                //We want to show percentage
                double res = (double)hours.TotalTimeInHrs / (double)sumOfHours * 100;
                point.YValues = new double[] { res };
                point.LabelFormat = "{##.#}%";
                series.Points.Add(point);
            }

            ChartSeriesData.Add(series);
        }
    }
}