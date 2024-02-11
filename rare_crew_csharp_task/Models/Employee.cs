using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rare_crew_csharp_task.Models
{
    public class Employee
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public string StarTimeUtc { get; set; }
        public string EndTimeUtc { get; set; }
    }
}