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

namespace rare_crew_csharp_task.Controllers
{
    public class HomeController : Controller
    {
        private string baseUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

        public async Task<ActionResult> Index()
        {
            List<Employee> employees = new List<Employee>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync(baseUrl);

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;

                    employees = JsonConvert.DeserializeObject<List<Employee>>(response);
                }
            }

            List<EmployeeViewModel> employeesToDisplay = new List<EmployeeViewModel>();

            foreach (var emp in employees)
            {
                if (employeesToDisplay.Count > 0)
                {
                    bool found = false;
                    for (int i = 0; i < employeesToDisplay.Count; i++)
                    {
                        if (emp.EmployeeName == employeesToDisplay[i].EmployeeName)
                        {
                            found = true;
                            employeesToDisplay[i].TotalTimeInHrs += Helper.Helper.calculateHours(emp.StarTimeUtc, emp.EndTimeUtc);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (!found)
                    {
                        var newEmp = new EmployeeViewModel
                        {
                            EmployeeName = emp.EmployeeName,
                            TotalTimeInHrs = Helper.Helper.calculateHours(emp.StarTimeUtc, emp.EndTimeUtc)
                        };
                        employeesToDisplay.Add(newEmp);
                    }
                }
                else
                {
                    var newEmp = new EmployeeViewModel
                    {
                        EmployeeName = emp.EmployeeName,
                        TotalTimeInHrs = Helper.Helper.calculateHours(emp.StarTimeUtc, emp.EndTimeUtc)
                    };
                    employeesToDisplay.Add(newEmp);
                }
            }

            return View(employeesToDisplay.OrderByDescending(emp => emp.TotalTimeInHrs));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}