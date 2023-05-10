namespace BlazorComponent
{
    public interface ITimePickerClock : IHasProviderComponent
    {
        int Min { get; }

        int Max { get; }

        int Step { get; }

        Func<int, string>? Format { get; }
    }
}
