using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SW.Payroll.ZoneService;

namespace SW.Payroll
{
    
    public class ZoneService : IzoneServices
    {
        private static Random random = new Random();
        public bool IsDengerZone(string dutyStation)
        {
            return random.Next(1, 10) == 3; 
        }

      
    }
}
