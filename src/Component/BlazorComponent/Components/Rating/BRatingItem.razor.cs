namespace BlazorComponent
{
    public partial class BRatingItem<TRating> where TRating : IRating
    {
        [Parameter]
        public int ItemIndex { get; set; }

        public RenderFragment<RatingItem> ItemContent => Component.ItemContent;

        public string GetIconName(RatingItem item) => Component.GetIconName(item);

        public RatingItem Item => Component.CreateProps(ItemIndex);
    }
}
