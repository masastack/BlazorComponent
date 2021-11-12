using System.Collections.Generic;

namespace BlazorComponent
{
    public class CalendarEventParsed
    {
        public Dictionary<string, object> Input { get; set; }

        public CalendarTimestamp Start { get; set; }

        public int StartIdentifier { get; set; }

        public int StartTimestampIdentifier { get; set; }

        public CalendarTimestamp End { get; set; }

        public int EndIdentifier { get; set; }

        public int EndTimestampIdentifier { get; set; }

        public bool AllDay { get; set; }

        public int Index { get; set; }

        public StringBoolean Category { get; set; }
    }
}
