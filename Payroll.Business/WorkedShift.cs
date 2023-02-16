using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Business
{
    public class WorkedShift
    {
        private int _startHour;
        private int _endHour;

        public DayOfWeek DayOfWeek { get; set; }
        public int StartHour
        {
            get { return _startHour; }
            set
            {
                if (value < 0 || value > 23)
                    throw new Exception("StartHour can't be less than 0 or greater than 23");

                if (value >= _endHour)
                    throw new Exception("StartHour must be less than EndHour");

                _startHour = value;
            }
        }
        public int EndHour
        {
            get { return _endHour; }
            set
            {
                if (value < 0 || value > 23)
                    throw new Exception("EndHour can't be less than 0 or greater than 23");

                if (value <= _startHour)
                    throw new Exception("EndHour must be greater than StartHour");

                _endHour = value;
            }
        }

        public WorkedShift(DayOfWeek dayOfWeek, int startHour, int endHour)
        {
            DayOfWeek = dayOfWeek;
            EndHour = endHour;
            StartHour = startHour;
        }
    }
}
