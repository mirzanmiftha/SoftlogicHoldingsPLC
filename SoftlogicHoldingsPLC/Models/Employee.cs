using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftlogicHoldingsPLC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmpoyeeName { get; set; }
        public string Department { get; set; }
        public string DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
