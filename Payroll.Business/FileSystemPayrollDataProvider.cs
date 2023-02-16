using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Business
{
    public class FileSystemPayrollDataProvider : IPayrollDataProvider
    {
        public string FilePath { get; set; }

        public List<EmployeeWorkedInfo> GetPayrollData()
        {
            List<EmployeeWorkedInfo> employeeWorkedInfoList = new List<EmployeeWorkedInfo>();
            EmployeeWorkedInfo employeeWorkedInfo;

            using (var sr = new StreamReader(FilePath))
            {
                while (sr.Peek() >= 0)
                {
                    employeeWorkedInfo = ParseEmployeeWorkedInfo(sr.ReadLine());

                    if (employeeWorkedInfo != null)
                        employeeWorkedInfoList.Add(employeeWorkedInfo);
                }
            }

            return employeeWorkedInfoList;
        }

        private EmployeeWorkedInfo ParseEmployeeWorkedInfo(string? line)
        {
            if (string.IsNullOrEmpty(line) || !line.Contains("="))
                return null;

            var parts = line.Split("=");

            var employeeName = String.IsNullOrEmpty(parts[0]) ? "UNKNOWN" : parts[0];
            var workedShifts = ParseWorkedShifts(parts[1]);

            if (!workedShifts.Any())
                return null;

            return new EmployeeWorkedInfo()
            {
                Name = employeeName,
                WorkedShifts = workedShifts
            };
        }

        private List<WorkedShift> ParseWorkedShifts(string workedShiftsSegment)
        {
            List<WorkedShift> workedShifts = new List<WorkedShift>();

            if (string.IsNullOrEmpty(workedShiftsSegment))
                return workedShifts;

            var splittedWorkedShifts = workedShiftsSegment.Split(",");

            foreach (var splittedWorkedShift in splittedWorkedShifts)
            {
                var workedShift = ParseWorkedShift(splittedWorkedShift);

                if (workedShift != null)
                    workedShifts.Add(workedShift);
            }

            return workedShifts;
        }

        private WorkedShift ParseWorkedShift(string workedShiftSegment)
        {
            DayOfWeek? dayOfWeek;
            int startHour, endHour;

            if (string.IsNullOrEmpty(workedShiftSegment) || workedShiftSegment.Length != 13)
                return null;

            dayOfWeek = ParseDayOfWeek(workedShiftSegment.Substring(0, 2));

            if (dayOfWeek == null || !int.TryParse(workedShiftSegment.Substring(2, 2), out startHour) || !int.TryParse(workedShiftSegment.Substring(8, 2), out endHour))
                return null;

            return new WorkedShift(dayOfWeek.Value, startHour, endHour);
        }

        private DayOfWeek? ParseDayOfWeek(string dayOfWeekSegment)
        {
            DayOfWeek? dayOfWeek;

            switch (dayOfWeekSegment)
            {
                case "MO":
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case "TU":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case "WE":
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case "TH":
                    dayOfWeek = DayOfWeek.Thursday;
                    break;
                case "FR":
                    dayOfWeek = DayOfWeek.Friday;
                    break;
                case "SA":
                    dayOfWeek = DayOfWeek.Saturday;
                    break;
                case "SU":
                    dayOfWeek = DayOfWeek.Sunday;
                    break;
                default:
                    dayOfWeek = null;
                    break;
            }

            return dayOfWeek;
        }
    }
}
