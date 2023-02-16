using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Business
{
    public class FakePayrollDataProvider : IPayrollDataProvider
    {
        public List<EmployeeWorkedInfo> PayrollData { get; set; }

        public FakePayrollDataProvider()
        {
            PayrollData = new List<EmployeeWorkedInfo>();
        }

        public List<EmployeeWorkedInfo> GetPayrollData()
        {
            return PayrollData;
        }
    }
}
