using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Business
{
    public class EmployeeWorkedInfo
    {
        public string Name { get; set; }
        public List<WorkedShift> WorkedShifts { get; set; }
    }
}
