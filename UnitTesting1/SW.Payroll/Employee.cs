using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Payroll
{
    public class Employee
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string DutyStation { get; set; }
        public bool IsMarried { get; set; }

        public int TotalDependancies { get; set; }
        public decimal Wage { get; set; }
        public int WorkingDays { get; set; }
        public bool IsDanger { get; set; }

        public bool HaspensionPlan { get; set; }

        public HealthInsurancePackage? HealthInsurancePackage { get; set; }
        public WorkPlatform workPlatform { get; set; }
    }
}
