using OneOf;
using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public class CalendarRenderProps
    {
        public CalendarTimestamp Start { get; set; }

        public CalendarTimestamp End { get; set; }

        public Type Component { get; set; }

        public int MaxDays { get; set; }

        public List<int> WeekDays { get; set; }

        public List<OneOf<string, Dictionary<string, object>>> Categories { get; set; }
    }
}
