using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class CalendarCommon
    {
        private const int MillisInDay = 86400000;

        public static Func<int, CalendarOverlapGroupHandler> GetOverlapGroupHandler => firstWeekday =>
        {
            var handler = new CalendarOverlapGroupHandler
            {
                Groups = new List<CalendarColumnGroup>(),
                Min = -1,
                Max = 1
            };

            handler.Reset = () =>
            {
                handler.Groups = new List<CalendarColumnGroup>();
                handler.Min = handler.Max - 1;
            };

            handler.GetVisuals = (day, dayEvents, timed, reset) =>
            {
                if(day.WeekDay == firstWeekday || reset)
                    handler.Reset();

                var dayStart = CalendarTimestampUtils.GetTimestampIdentifier(day);
                var visuals = GetVisuals(dayEvents, dayStart);

                visuals.ForEach(visual =>
                {
                    var (start, end) = timed ? GetRange(visual.Event) : GetDayRange(visual.Event);
                    if (handler.Groups.Count > 0 && !HasOverlap(start, end, handler.Min, handler.Max, timed))
                    {
                        SetColumnCount(handler.Groups);
                        handler.Reset();
                    }

                    var targetGroup = GetOpenGroup(handler.Groups, start, end, timed);
                    if (targetGroup == -1)
                    {
                        targetGroup = handler.Groups.Count;
                        handler.Groups.Add(new CalendarColumnGroup
                        {
                            Start = start,
                            End = end
                        });
                    }

                    var target = handler.Groups[targetGroup];
                    target.Visuals ??= new List<CalendarEventVisual>();
                    target.Visuals.Add(visual);
                    target.Start = Math.Min(target.Start, start);
                    target.End = Math.Max(target.End, end);

                    visual.Column = targetGroup;

                    if (handler.Min == -1)
                    {
                        handler.Min = start;
                        handler.Max = end;
                    }
                    else
                    {
                        handler.Min = Math.Min(handler.Min, start);
                        handler.Max = Math.Max(handler.Max, end);
                    }
                });

                SetColumnCount(handler.Groups);

                if(timed)
                    handler.Reset();

                return visuals;
            };

            return handler;
        };

        public static (int, int) GetRange(CalendarEventParsed @event) =>
            (@event.StartTimestampIdentifier, @event.EndTimestampIdentifier);

        public static (int, int) GetDayRange(CalendarEventParsed @event) =>
            (@event.StartIdentifier, @event.EndIdentifier);

        public static (int, int) GetNormalizedRange(CalendarEventParsed @event, int dayStart) =>
            (Math.Max(dayStart, @event.StartTimestampIdentifier), Math.Min(dayStart + MillisInDay, @event.EndTimestampIdentifier));

        public static int GetOpenGroup(List<CalendarColumnGroup> groups, int start, int end, bool timed)
        {
            for (int i = 0; i < groups.Count; i++)
            {
                var group = groups[i];
                var intersected = false;

                if (HasOverlap(start, end, group.Start, group.End, timed))
                {
                    for (int j = 0; j < group.Visuals.Count; j++)
                    {
                        var groupVisual = group.Visuals[j];
                        var (groupStart, groupEnd) = timed ? GetRange(groupVisual.Event) : GetDayRange(groupVisual.Event);

                        if (HasOverlap(start, end, groupStart, groupEnd, timed))
                        { 
                            intersected = true;
                            break;
                        }
                    }
                }

                if (!intersected)
                    return i;
            }

            return -1;
        }

        public static bool HasOverlap(int s0, int e0, int s1, int e1, bool excule = true) =>
            excule ? !(s0 >= e1 || e0 <= s1) : !(s0 > e1 || e0 < s1);

        public static void SetColumnCount(List<CalendarColumnGroup> groups)
        {
            groups.ForEach(group =>
            {
                group.Visuals.ForEach(visual =>
                {
                    visual.ColumnCount = groups.Count;
                });
            });
        }


        public static List<CalendarEventVisual> GetVisuals(List<CalendarEventParsed> events, int minStart = 0)
        {
            var visuals = events.Select(x => new CalendarEventVisual
            {
                Event = x,
                ColumnCount = 0,
                Column = 0,
                Left = 0,
                Width = 0
            }).ToList();

            for (int i = 0; i < visuals.Count - 1; i++)
            {
                for (int j = 0; j < visuals.Count - 1 - i; j++)
                {
                    if ((Math.Max(minStart, visuals[i].Event.StartTimestampIdentifier) - 
                        Math.Max(minStart, visuals[j].Event.StartTimestampIdentifier)) > 0 ||
                       (visuals[j].Event.EndTimestampIdentifier - visuals[i].Event.EndTimestampIdentifier) > 0)
                    {
                        var temp = visuals[j];
                        visuals[j] = visuals[j + 1];
                        visuals[j + 1] = temp;
                    }
                }
            }

            return visuals;
        }
    }
}
