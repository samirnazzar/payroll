using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Business
{
    public interface IPayrollDataProvider
    {
        List<EmployeeWorkedInfo> GetPayrollData();
    }
}
