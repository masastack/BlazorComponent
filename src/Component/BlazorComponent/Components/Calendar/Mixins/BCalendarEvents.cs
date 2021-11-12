using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent
{
    public partial class BCalendarEvents : BCalendarBase
    {
        #region Event

        [Parameter]
        public List<Dictionary<string, object>> Events { get; set; }

        [Parameter]
        public string EventStart { get; set; } = "start";

        [Parameter]
        public string EventEnd { get; set; } = "end";

        [Parameter]
        public OneOf<string, Func<Dictionary<string, object>, bool>> EventTimed { get; set; } = "timed";

        [Parameter]
        public OneOf<string, Func<Dictionary<string, object>, string>> EventCategory { get; set; } = "category";

        [Parameter]
        public int EventHeight { get; set; } = 20;

        [Parameter]
        public OneOf<string, Func<Dictionary<string, object>, string>> EventColor { get; set; } = "primary";

        [Parameter]
        public OneOf<string, Func<Dictionary<string, object>, string>> EventTextColor { get; set; } = "white";

        [Parameter]
        public OneOf<string, Func<CalendarEventParsed, bool, string>> EventName { get; set; } = "name";

        [Parameter]
        public StringNumber EventOverlapThreshold { get; set; } = 60;

        [Parameter]
        public OneOf<string, Func<List<CalendarEventParsed>, int, int,
            Func<CalendarDaySlotScope, List<CalendarEventParsed>, bool, bool, List<CalendarEventVisual>>>> EventOverlapMode { get; set; } = "stack";

        [Parameter]
        public bool EventMore { get; set; } = true;

        [Parameter] //TODO '$vuetify.calendar.moreEvents'
        public string EventMoreText { get; set; }

        [Parameter]
        public OneOf<bool, object> EventRipple { get; set; }

        [Parameter]
        public int EventMarginBottom { get; set; } = 1;

        #endregion

        #region Calendar

        [Parameter]
        public string Type { get; set; } = "month";

        [Parameter]
        public StringNumberDate Value { get; set; }

        [Parameter]
        public EventCallback<StringNumberDate> ValueChanged { get; set; }

        #endregion

        #region Category

        [Parameter]
        public OneOf<string, List<OneOf<string, Dictionary<string, object>>>> Categories { get; set; }

        [Parameter]
        public OneOf<string, Func<OneOf<string, Dictionary<string, object>>, string>> CategoryText { get; set; }

        [Parameter]
        public bool CategoryHideDynamic { get; set; }

        [Parameter]
        public bool CategoryShowAll { get; set; }

        [Parameter]
        public string CategoryForInvalid { get; set; } = string.Empty;

        [Parameter]
        public StringNumber CategoryDays { get; set; } = 1;

        #endregion

        [Parameter]
        public RenderFragment<EventSlotScope> EventContent { get; set; }

        public ElementReference EventsRef { get; set; }

        public const int WidthFull = 100;
        public const int WidthStart = 95;
        public const int MinutesInDay = 1440;

        public bool NoEvents => Events.Count == 0;

        public List<CalendarEventParsed> ParsedEvents()
        {
            var res = new List<CalendarEventParsed>();
            for (int i = 0; i < Events.Count; i++)
                res.Add(ParseEvent(Events[i], i));

            return res;
        }

        public int ParsedEventOverlapThreshold => EventOverlapThreshold.ToInt32();

        public Func<Dictionary<string, object>, bool> EventTimedFunction =>
            EventTimed.IsT1 ? EventTimed.AsT1 : @event => 
            @event.ContainsKey(EventTimed.AsT0) && @event[EventTimed.AsT0] != null;

        public Func<Dictionary<string, object>, string> EventCategoryFunction =>
            EventCategory.IsT1 ? EventCategory.AsT1 : @event => @event[EventCategory.AsT0].ToString();

        public Func<Dictionary<string, object>, string> EventTextColorFunction =>
            EventTextColor.IsT1 ? EventTextColor.AsT1 : @event => EventTextColor.AsT0;

        public Func<CalendarEventParsed, bool, string> EventNameFunction =>
            EventName.IsT1 ? EventName.AsT1 : (@event, timedEvent) => EscapeHTML(@event.Input[EventName.AsT0].ToString());

        public Func<List<CalendarEventParsed>, int, int,
            Func<CalendarDaySlotScope, List<CalendarEventParsed>, bool, bool, List<CalendarEventVisual>>> EventModeFunction =>
            EventOverlapMode.IsT1 ? EventOverlapMode.AsT1 : CalendarIndex.CalendarEventOverlapModes[EventOverlapMode.AsT0];

        public virtual List<int> EventWeekdays => ParsedWeekdays();

        public virtual bool CategoryMode => Type.Equals("category");

        public string EventColorFunction(Dictionary<string, object> e) =>
            EventColor.IsT1 ? EventColor.AsT1(e) : (e.ContainsKey("color") ? e["color"].ToString() : EventColor.AsT0);

        public CalendarEventParsed ParseEvent(Dictionary<string, object> input, int index = 0) =>
            CalendarEvents.ParseEvent(input, index, EventStart, EventEnd, 
                EventTimedFunction(input), CategoryMode ? EventCategoryFunction(input) : false);

        private static string EscapeHTML(string str) =>
            string.IsNullOrWhiteSpace(str) ? str :
                 str.Replace("&", "&amp;")
                 .Replace("<", "&lt;")
                 .Replace(">", "&gt;");

        public Func<CalendarDaySlotScope, List<CalendarEventParsed>> GetEventsForDayAll => day =>
        {
            var identifier = CalendarTimestampUtils.GetDayIdentifier(day);
            var firstWeekly = EventWeekdays[0];

            var parsedEvents = new List<CalendarEventParsed>();
            ParsedEvents().ForEach(@event =>
            {
                if (@event.AllDay &&
                (CategoryMode ? CalendarEvents.IsEventOn(@event, identifier) : CalendarEvents.IsEventStart(@event, day, identifier, firstWeekly)) &&
                IsEventForCategory(@event, day.Category))
                    parsedEvents.Add(@event);
            });

            return parsedEvents;
        };

        public bool IsEventForCategory(CalendarEventParsed @event, OneOf<string, Dictionary<string, object>> category)
        {
            return !CategoryMode ||
                (category.IsT1 && category.AsT1.ContainsKey(CalendarParser.CalendarCategoryCategoryName) &&
                (StringBoolean)category.AsT1[CalendarParser.CalendarCategoryCategoryName] == @event.Category) ||
                (category.IsT0 && category.AsT0 == @event.Category);
        }

        public Func<CalendarEventVisual, CalendarDaySlotScope, RenderFragment> GenDayEvent => (@event, day) =>
        {
            var eventHeight = EventHeight;
            var eventMarginBottom = EventMarginBottom;
            var dayIdentifier = CalendarTimestampUtils.GetDayIdentifier(day);
            var week = day.Week;
            var start = dayIdentifier == @event.Event.StartIdentifier;
            var end = dayIdentifier == @event.Event.EndIdentifier;
            var width = WidthStart;


            if (!CategoryMode)
            {
                for (int i = day.Index + 1; i < week.Count; i++)
                {
                    var weekdayIdentifier = CalendarTimestampUtils.GetDayIdentifier(week[i]);
                    if (@event.Event.EndIdentifier >= weekdayIdentifier)
                    {
                        width += WidthFull;
                        end = end || weekdayIdentifier == @event.Event.EndIdentifier;
                    }
                    else
                    {
                        end = true;
                        break;
                    }
                }
            }

            var scope = new EventScopeInput { EventParsed = @event.Event, Day = day, Start = start, End = end, Timed = false };
            var data = new Dictionary<string, object>
            {
                { "class", $"m-event {(start ? "m-event-start": string.Empty)} {(end ? "m-event-end": string.Empty)}"},
                { "style", $"height:{eventHeight}px; width:{width}px; margin-bottom:{eventMarginBottom}px;"},
                { "ref", EventsRef}
            };

            return GenEvent(@event.Event, scope, false, data);
        };

        public RenderFragment GenEvent(CalendarEventParsed @event, 
            EventScopeInput scopeInput, bool timedEvent, Dictionary<string, object> data)
        {
            var slot = EventContent;
            var text = EventTextColorFunction(@event.Input);
            var background = EventColorFunction(@event.Input);
            var overlapsNoon = @event.Start.Hour < 12 && @event.End.Hour >= 12;
            var singline = CalendarTimestampUtils.DiffMinutes(@event.Start, @event.End) <= ParsedEventOverlapThreshold;
            var formatTime = FormatTime;
            var timeSummary = () => formatTime(@event.Start, overlapsNoon) + " - " + formatTime(@event.End, true);
            var eventSummary = () =>
            {
                var name = EventNameFunction(@event, timedEvent);

                if (@event.Start.HasTime)
                {
                    if (timedEvent)
                    {
                        var time = timeSummary();
                        var delimiter = singline ? ", " : "<br>";

                        return $"<strong>{name}</strong> {delimiter} {time}";
                    }
                    else
                    {
                        var time = formatTime(@event.Start, true);

                        return $"<strong>{time}</strong> {name}";
                    }
                }

                return name;
            };

            var scope = new EventSlotScope
            {
                Event = @event.Input,
                Outside = scopeInput.Day.Outside,
                SingleLine = singline,
                OverlapsNoon = overlapsNoon,
                FormatTime = formatTime,
                TimeSummary = timeSummary,
                EventSummary = eventSummary,
                EventParsed = scopeInput.EventParsed,
                Day = scopeInput.Day,
                Start = scopeInput.Start,
                End = scopeInput.End,
                Timed = scopeInput.Timed
            };

            void res(RenderTreeBuilder builder)
            {
                var sequence = 0;
                builder.OpenElement(sequence, "div");
                foreach (var dic in data)
                {
                    builder.AddAttribute(sequence++, dic.Key, !"class".Equals(dic.Key) ? dic.Value :
                        $"{dic.Value} {(!string.IsNullOrWhiteSpace(text) ? $"{text}--text" : "")} {background}");
                }
                builder.AddContent(sequence++, slot != null ? slot(scope) : GenName(eventSummary));
                builder.CloseElement();
            }

            return res;
        }

        public Func<CalendarTimestamp, bool, string> FormatTime => (withTime, ampm) =>
        {
            return GetFormatter(new CalendarFormatterOptions
            {
                TimeZone = "UTC",
                Hour = "numeric",
                Minute = withTime.Minute > 0 ? "numeric" : null
            }, withTime, true);
        };

        public static Func<Func<string>, RenderFragment> GenName => eventSummary =>
        {
            void res(RenderTreeBuilder builder)
            {
                builder.AddMarkupContent(0, $"<div class=\"pl-1\">{eventSummary()}</div>");
            }

            return res;
        };

        public Func<CalendarDaySlotScope, List<CalendarEventParsed>, bool, bool, List<CalendarEventVisual>> Mode =>
            EventModeFunction(ParsedEvents(), EventWeekdays[0], ParsedEventOverlapThreshold);

        public Func<CalendarDaySlotScope, List<CalendarEventParsed>> GetEventsForDayTimed => day =>
        {
            var identifier = CalendarTimestampUtils.GetDayIdentifier(day);

            var parsedEvents = new List<CalendarEventParsed>();
            ParsedEvents().ForEach(@event =>
            {
                if (!@event.AllDay && CalendarEvents.IsEventOn(@event, identifier) &&
                IsEventForCategory(@event, day.Category))
                    parsedEvents.Add(@event);
            });

            return parsedEvents;
        };

        public Func<CalendarEventVisual, CalendarDayBodySlotScope, RenderFragment> GenTimedEvent => (@event, day) =>
        {
            var timeDeltaEnd = day.TimeDelta(@event.Event.End);
            var timeDeltaStart = day.TimeDelta(@event.Event.Start);
            if (((timeDeltaEnd.IsT0 && timeDeltaEnd.AsT0 < 0) || timeDeltaEnd.IsT1 && !timeDeltaEnd.AsT1) ||
                ((timeDeltaStart.IsT0 && timeDeltaStart.AsT0 >= 1) || timeDeltaStart.IsT1 && !timeDeltaStart.AsT1) ||
                CalendarEvents.IsEventHiddenOn(@event.Event, day))
                return null;

            var dayIdentifier = CalendarTimestampUtils.GetDayIdentifier(day);
            var start = @event.Event.StartIdentifier >= dayIdentifier;
            var end = @event.Event.EndIdentifier > dayIdentifier;
            var top = start ? day.TimeToY(@event.Event.Start, true) : 0;
            var bottom = end ? day.TimeToY((StringNumber)MinutesInDay, true) : day.TimeToY(@event.Event.End, true);
            var height = Math.Max(EventHeight, bottom - top);

            var scope = new EventScopeInput { EventParsed = @event.Event, Day = day, Start = start, End = end, Timed = true };
            var data = new Dictionary<string, object>
            {
                { "class", $"m-event-timed"},
                { "style", $"top: {top}px;height:{height}px; left:{@event.Left}%; width:{@event.Width}%;"}
            };

            return GenEvent(@event.Event, scope, true, data);
        };

        public Func<CalendarDaySlotScope, List<CalendarEventParsed>> GetEventsForDay => day =>
        {
            var identifier = CalendarTimestampUtils.GetDayIdentifier(day);
            var firstWeekly = EventWeekdays[0];

            var parsedEvents = new List<CalendarEventParsed>();
            ParsedEvents().ForEach(@event =>
            {
                if (CalendarEvents.IsEventStart(@event, day, identifier, firstWeekly))
                    parsedEvents.Add(@event);
            });

            return parsedEvents;
        };
    }
}
 