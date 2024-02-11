using Newtonsoft.Json;
using rare_crew_csharp_task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using rare_crew_csharp_task.Helper;
using System.Configuration;

namespace rare_crew_csharp_task.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {

            List<EmployeeViewModel> employeesToDisplay = await DataRetriever.RetrieveDataToDisplayAsync();

            return View(employeesToDisplay);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public async Task<FileResult> IndexChart()
        {
            List<EmployeeViewModel> employeesToDisplay = await DataRetriever.RetrieveDataToDisplayAsync();

            EmployeeTotalHoursChartData employeesData = new EmployeeTotalHoursChartData();
            employeesData.Title = ConfigurationManager.AppSettings["ChartTitle"];
            employeesData.Height = int.Parse(ConfigurationManager.AppSettings["ChartHeight"]);
            employeesData.Width = int.Parse(ConfigurationManager.AppSettings["ChartWidth"]);
            employeesData.EmployeeData = new List<EmployeeViewModel>();
            employeesData.EmployeeData = employeesToDisplay;

            byte[] bytes = employeesData.ChartImageStream().GetBuffer();

            return File(employeesData.ChartImageStream().GetBuffer()
                , @"image/png", "EmployeesWorkHoursChart.png");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}