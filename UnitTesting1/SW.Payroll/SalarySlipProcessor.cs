using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Payroll
{
    public class SalarySlipProcessor
    {
        private readonly IzoneServices _izoneServices; 
      public SalarySlipProcessor(IzoneServices izoneServices) {
        _izoneServices = izoneServices;     
        }      
        public decimal CalculateBasicSalary(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            return employee.Wage * employee.WorkingDays;
        }
        public decimal CalculateTransportationAllowance(Employee employee)
        {

            if (employee == null) {
                throw new ArgumentNullException(nameof(employee));
            }
            if (employee.workPlatform == WorkPlatform.Office)
            {
                return Constants.TransportationAllowanceAmount;
            }
            if (employee.workPlatform == WorkPlatform.Remote)
            {
                return 0m;
            }

            return Constants.TransportationAllowanceAmount / 2;
        }

        public decimal CalculateDangerPay(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));  
            }
            if (employee.IsDanger)
            {
                return Constants.DangerPayAmount;
            }
           

            var isDangerZone = _izoneServices.IsDengerZone(employee.DutyStation); 
            if (isDangerZone) {
                return Constants.DangerPayAmount;
            }

            return 0m;
        }
    }

}
