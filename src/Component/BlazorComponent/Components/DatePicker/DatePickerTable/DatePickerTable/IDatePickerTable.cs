namespace BlazorComponent;

public interface IDatePickerTable : IHasProviderComponent
{
    int DisplayedYear { get; }

    Dictionary<string, object> GetButtonAttrs(DateOnly value);

    Func<DateOnly, string> Formatter { get; }

    OneOf<DateOnly[], Func<DateOnly, bool>>? Events { get; }

    OneOf<string, Func<DateOnly, string>, Func<DateOnly, string[]>>? EventColor { get; }
}