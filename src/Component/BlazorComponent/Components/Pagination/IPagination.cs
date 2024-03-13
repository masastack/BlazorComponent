namespace BlazorComponent
{
    public interface IPagination : IHasProviderComponent
    {
        string? HrefFormat { get; }
        
        string GetIcon(int index);

        IEnumerable<StringNumber> GetItems();

        bool PrevDisabled => default;

        bool NextDisabled => default;

        Task HandlePreviousAsync(MouseEventArgs args);

        Task HandleNextAsync(MouseEventArgs args);

        Task HandleItemClickAsync(StringNumber item);

        string? PrevIcon => default;

        int Value => default;
    }
}
