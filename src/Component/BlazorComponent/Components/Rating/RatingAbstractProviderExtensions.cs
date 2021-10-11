namespace BlazorComponent
{
    public static class RatingAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyRatingDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BRatingItem<>), typeof(BRatingItem<IRating>));
        }
    }
}
