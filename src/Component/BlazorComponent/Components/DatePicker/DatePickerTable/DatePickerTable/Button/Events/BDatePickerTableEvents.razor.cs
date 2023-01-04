namespace BlazorComponent;

public partial class BDatePickerTableEvents<TDatePickerTable> where TDatePickerTable : IDatePickerTable
{
    [Parameter]
    public DateOnly Date { get; set; }

    public OneOf<DateOnly[], Func<DateOnly, bool>>? Events => Component.Events;

    public OneOf<string, Func<DateOnly, string>, Func<DateOnly, string[]>>? EventColor => Component.EventColor;

    private IEnumerable<string> GetEventColors()
    {
        var eventData = false;
        var eventColors = Array.Empty<string>();

        if (!Events.HasValue)
        {
            eventData = false;
        }
        else if (Events.Value.IsT0)
        {
            eventData = Events.Value.AsT0.Contains(Date);
        }
        else if (Events.Value.IsT1)
        {
            eventData = Events.Value.AsT1.Invoke(Date);
        }

        if (!eventData || !EventColor.HasValue)
        {
            return eventColors;
        }

        if (EventColor.Value.IsT0)
        {
            eventColors = new[] { EventColor.Value.AsT0 };
        }
        else if (EventColor.Value.IsT1)
        {
            eventColors = new[] { EventColor.Value.AsT1.Invoke(Date) };
        }
        else if (EventColor.Value.IsT2)
        {
            eventColors = EventColor.Value.AsT2.Invoke(Date).ToArray();
        }

        return eventColors.Where(e => !string.IsNullOrWhiteSpace(e));
    }
}
