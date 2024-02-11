using Newtonsoft.Json;
using rare_crew_csharp_task.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;

namespace rare_crew_csharp_task.Helper
{
    public class DataRetriever
    {
        public static async Task<List<EmployeeViewModel>> RetrieveDataToDisplayAsync()
        {
            List<Employee> employees = new List<Employee>();

            var URL = ConfigurationManager.AppSettings["URL"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await client.GetAsync(URL);

                if (res != null)
                {
                    if (res.IsSuccessStatusCode)
                    {
                        var response = res.Content.ReadAsStringAsync().Result;

                        if (response != null)
                        {
                            employees = JsonConvert.DeserializeObject<List<Employee>>(response);
                        }
                    }
                }
            }

            List<EmployeeViewModel> employeesToDisplay = new List<EmployeeViewModel>();

            if (employees.Count > 0) 
            {
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
                                //There are some wrong values that have bigger start time than end time
                                if (Helper.CalculateHours(emp.StarTimeUtc, emp.EndTimeUtc) > 0)
                                    employeesToDisplay[i].TotalTimeInHrs += Helper.CalculateHours(emp.StarTimeUtc, emp.EndTimeUtc);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (!found)
                        {
                            var timeDiff = Helper.CalculateHours(emp.StarTimeUtc, emp.EndTimeUtc);
                            //There are some wrong values that have bigger start time than end time
                            if (timeDiff < 0)
                            {
                                continue;
                            }

                            var newEmp = new EmployeeViewModel
                            {
                                EmployeeName = emp.EmployeeName,
                                TotalTimeInHrs = Helper.CalculateHours(emp.StarTimeUtc, emp.EndTimeUtc)
                            };

                            employeesToDisplay.Add(newEmp);
                        }
                    }
                    else
                    {
                        var timeDiff = Helper.CalculateHours(emp.StarTimeUtc, emp.EndTimeUtc);
                        //There are some wrong values that have bigger start time than end time
                        if (timeDiff < 0)
                        {
                            continue;
                        }

                        var newEmp = new EmployeeViewModel
                        {
                            EmployeeName = emp.EmployeeName,
                            TotalTimeInHrs = Helper.CalculateHours(emp.StarTimeUtc, emp.EndTimeUtc)
                        };
                        employeesToDisplay.Add(newEmp);
                    }
                }
            }

            return employeesToDisplay.OrderByDescending(emp => emp.TotalTimeInHrs).ToList();
        }
    }
}