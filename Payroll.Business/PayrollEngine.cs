using System.Collections.Generic;
using System.Xml.Linq;

namespace Payroll.Business
{
    public class PayrollEngine
    {
        private IPayrollDataProvider _payrollDataProvider;

        public PayrollEngine(IPayrollDataProvider payrollDataProvider)
        {
            _payrollDataProvider = payrollDataProvider;
        }

        public List<CalculatePayResult> CalculatePay()
        {
            List <CalculatePayResult> results = new List<CalculatePayResult>();
            int payAmount;

            foreach (var employee in _payrollDataProvider.GetPayrollData())
            {
                payAmount = 0;

                foreach (var workedShift in employee.WorkedShifts)
                {
                    payAmount = payAmount + CalculateWorkshiftPayAmount(workedShift);
                }

                results.Add(new CalculatePayResult()
                {
                    Name = employee.Name,
                    Amount = payAmount
                });
            }

            return results;
        }

        private int CalculateWorkshiftPayAmount(WorkedShift workedShift)
        {
            int hoursWorked, payAmount = 0;

            if (workedShift.DayOfWeek == DayOfWeek.Monday ||
                workedShift.DayOfWeek == DayOfWeek.Tuesday ||
                workedShift.DayOfWeek == DayOfWeek.Wednesday ||
                workedShift.DayOfWeek == DayOfWeek.Thursday ||
                workedShift.DayOfWeek == DayOfWeek.Friday)
            {
                hoursWorked = CalculateHoursWorkedBetween(workedShift, 0, 9);
                payAmount = payAmount + (hoursWorked * 25);
                hoursWorked = CalculateHoursWorkedBetween(workedShift, 9, 18);
                payAmount = payAmount + (hoursWorked * 15);
                hoursWorked = CalculateHoursWorkedBetween(workedShift, 18, 24);
                payAmount = payAmount + (hoursWorked * 20);
            }
            else if (workedShift.DayOfWeek == DayOfWeek.Saturday ||
                    workedShift.DayOfWeek == DayOfWeek.Sunday)
            {
                hoursWorked = CalculateHoursWorkedBetween(workedShift, 0, 9);
                payAmount = payAmount + (hoursWorked * 30);
                hoursWorked = CalculateHoursWorkedBetween(workedShift, 9, 18);
                payAmount = payAmount + (hoursWorked * 20);
                hoursWorked = CalculateHoursWorkedBetween(workedShift, 18, 24);
                payAmount = payAmount + (hoursWorked * 25);
            }

            return payAmount;
        }

        private int CalculateHoursWorkedBetween(WorkedShift workedShift, int scheduleStartHour, int scheduleEndHour)
        {
            int startHour, endHour, hoursWorked = 0;

            if (workedShift.StartHour >= scheduleStartHour && workedShift.StartHour < scheduleEndHour)
            {
                startHour = workedShift.StartHour;
                endHour = workedShift.EndHour <= scheduleEndHour ? workedShift.EndHour : scheduleEndHour;
                hoursWorked = endHour - startHour;
            }
            else if (workedShift.EndHour >= scheduleStartHour && workedShift.EndHour < scheduleEndHour)
            {
                startHour = scheduleStartHour;
                endHour = workedShift.EndHour <= scheduleEndHour ? workedShift.EndHour : scheduleEndHour;
                hoursWorked = endHour - startHour;
            }

            return hoursWorked;
        }
    }
}