namespace SW.Payroll
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var employee = new Employee
            {
                Id = 1000,
                Name = "Reem A",
                DutyStation = "Montreal",
                IsMarried = true,
                TotalDependancies = 2,
                Wage = 910m,
                HaspensionPlan = true,
                IsDanger = true,
                HealthInsurancePackage = HealthInsurancePackage.Basic,
                WorkingDays = 22,
                workPlatform = WorkPlatform.Hybrid,
            };
        }
    }
}
