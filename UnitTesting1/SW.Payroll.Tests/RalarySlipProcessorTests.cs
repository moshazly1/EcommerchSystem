namespace SW.Payroll.Tests
{
    public class RalarySlipProcessorTests
    {
        //[Fact]
        //public void Method_Scenario_Outcome()
        //{

        //}

        [Fact]
        public void CalculateBasicSalary_EmployeeIsNull_ThrowArgumentNullException ()
        {
            //Arrange 
            Employee employee = null; 
         
            //Act 
            var salarySlipProcessor = new SalarySlipProcessor(null); 
           Func<Employee  , decimal > func  = (e)=> salarySlipProcessor.CalculateBasicSalary(employee);
            //Assert
            Assert.Throws<ArgumentNullException>(()=>func(employee)); 
        }


        [Fact] 
        public void CalculateTransportationAllowance_EmployeeIsNull_ThrowArgumentNullException()
        {
       //Arrange  
       Employee employee = null;       

       //Act

            var salarySlipProcessor = new  SalarySlipProcessor(null) ;
            Func<Employee, decimal> func = (e) => salarySlipProcessor.CalculateTransportationAllowance(employee);
            //Assert 
            Assert.Throws<ArgumentNullException>(() => func(employee));
        }


        [Fact]
        public void CalculateTransportationAllowance_ForEmployeeWageAndWorkingDays_ReturnsTransportationAllowance ()
        {
            //Arrange  
            var employee = new Employee
            {
                workPlatform = WorkPlatform.Office
            };     

            //Act

            var salarySlipProcessor = new SalarySlipProcessor(null);
            var Actual = salarySlipProcessor.CalculateTransportationAllowance(employee);
            var expected = Constants.TransportationAllowanceAmount; 
            //Assert 
            Assert.Equal(expected, Actual); 
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
            if (isDangerZone)
            {
                return Constants.DangerPayAmount;
            }

            return 0m;
        }

        //public decimal CalculateTransportationAllowance(Employee employee)
        //{

        //    if (employee == null)
        //    {
        //        throw new ArgumentNullException(nameof(employee));
        //    }
        //    if (employee.workPlatform == WorkPlatform.Office)
        //    {
        //        return 0m;
        //    }
        //    if (employee.workPlatform == WorkPlatform.Remote)
        //    {
        //        return Constants.TransportationAllowanceAmount;
        //    }

        //    return Constants.TransportationAllowanceAmount / 2;
        //}

        //[Fact]
        //public void CalculateBasicSalary_ForEmployeeObject_ReturnsBasicSalary()
        //{
        //    //Arrange 
        //    var employee = new Employee
        //    {
        //        Wage = 500m,
        //        WorkingDays = 20
        //    };
        //    //Act 
        //    var salarySlipProcessor = new SalarySlipProcessor();
        //    var actual = salarySlipProcessor.CalculateBasicSalary(employee);
        //    var expected = 10000m;

        //    //Assert
        //    Assert.Equal(expected, actual);
        //}


    }
}