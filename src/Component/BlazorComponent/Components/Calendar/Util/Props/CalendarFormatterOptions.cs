using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class CalendarFormatterOptions
    {
        public string TimeZone { get; set; }

        public string Weekday { get; set; }

        public string Day { get; set; }

        public string Hour { get; set; }

        public string Minute { get; set; }

        public string Month { get; set; }
    }
}
