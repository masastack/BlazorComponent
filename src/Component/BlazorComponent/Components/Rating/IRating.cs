namespace BlazorComponent
{
    public interface IRating : IHasProviderComponent
    {
        RenderFragment<RatingItem>? ItemContent { get; }

        RatingItem CreateProps(int i);

        string GetIconName(RatingItem item);
    }
}
