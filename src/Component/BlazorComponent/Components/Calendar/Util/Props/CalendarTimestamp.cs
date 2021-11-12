using OneOf;
using System.Collections.Generic;
using System;

namespace BlazorComponent
{
    public class CalendarTimestamp
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int WeekDay { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public bool HasDay { get; set; }

        public bool HasTime { get; set; }

        public bool Past { get; set; }

        public bool Present { get; set; }

        public bool Future { get; set; }

        public OneOf<string, Dictionary<string, object>> Category { get; set; }
    }
}
