using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
   

    public class SailingSchedule
    {
        public DateTime DepartureTime { get; }
        public TimeSpan Interval { get; }

        public SailingSchedule(DateTime departureTime, TimeSpan interval)
        {
            DepartureTime = departureTime;
            Interval = interval;
        }
    }

}
