namespace BlazorComponent
{
    public interface IDatePickerTable : IHasProviderComponent
    {
        int DisplayedYear { get; }

        Dictionary<string, object> GetButtonAttrs(DateOnly value);

        Func<DateOnly, string> Formatter { get; }
    }
}
