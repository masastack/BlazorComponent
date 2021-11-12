using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorComponent
{
    public static class CalendarEvents
    {
        public static CalendarEventParsed ParseEvent(Dictionary<string, object> input, int index,
            string startProperty, string endProperty, bool timed = false, StringBoolean category = default)
        {
            if (input == null || !input.Any() || !input.ContainsKey(startProperty))
                return null;

            var startInput = (StringNumberDate)Convert.ToDateTime(input[startProperty]);
            var endInput = !input.ContainsKey(endProperty) ? null : (StringNumberDate)Convert.ToDateTime(input[endProperty]);
            var startParsed = CalendarTimestampUtils.ParseTimestamp(startInput);
            var endParsed = endInput != null ? CalendarTimestampUtils.ParseTimestamp(endInput) : startParsed;
            var start = !startInput.IsT0 ? CalendarTimestampUtils.UpdateHasTime(startParsed, timed) : startParsed;
            var end = endInput != null && !endInput.IsT0 ? CalendarTimestampUtils.UpdateHasTime(endParsed, timed) : endParsed;
            var startIdentifier = CalendarTimestampUtils.GetDayIdentifier(start);
            var startTimestampIdentifier = CalendarTimestampUtils.GetTimestampIdentifier(start);
            var endIdentifier = CalendarTimestampUtils.GetDayIdentifier(end);
            var endOffset = start.HasTime ? 0 : 2359;
            var endTimestampIdentifier = CalendarTimestampUtils.GetTimestampIdentifier(end) + endOffset;
            var allDay = !start.HasTime;

            return new CalendarEventParsed
            {
                Input = input,
                Start = start,
                StartIdentifier = startIdentifier,
                StartTimestampIdentifier = startTimestampIdentifier,
                End = end,
                EndIdentifier = endIdentifier,
                EndTimestampIdentifier = endTimestampIdentifier,
                AllDay = allDay,
                Index = index,
                Category = category
            };
        }

        public static bool IsEventOn(CalendarEventParsed @event, int dayIdentifier) =>
            dayIdentifier >= @event.StartIdentifier && dayIdentifier <= @event.EndIdentifier;

        public static bool IsEventHiddenOn(CalendarEventParsed @event, CalendarTimestamp day) =>
            @event.End.Time == "00:00" && @event.End.Date == day.Date && @event.Start.Date != day.Date;

        public static bool IsEventStart(CalendarEventParsed @event, CalendarTimestamp day, int dayIdentifier, int firstWeekday) =>
            dayIdentifier == @event.StartIdentifier || (firstWeekday == day.WeekDay && IsEventOn(@event, dayIdentifier));

        public static bool IsEventOverlapping(CalendarEventParsed @event, int startIdentifier, int endIdentifier) =>
            startIdentifier <= @event.EndIdentifier && endIdentifier >= @event.StartIdentifier;
    }
}
