namespace BlazorComponent
{
    public interface IDatePickerHeader : IHasProviderComponent
    {
        bool RTL { get; }

        string PrevIcon { get; }

        string NextIcon { get; }

        string Transition { get; }

        DateOnly Value { get; }

        Dictionary<string, object> ButtonAttrs { get; }

        RenderFragment? ChildContent { get; }

        Func<DateOnly, string> Formatter { get; }
    }
}
