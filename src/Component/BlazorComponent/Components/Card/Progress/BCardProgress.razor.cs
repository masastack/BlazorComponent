namespace BlazorComponent
{
    public partial class BCardProgress<TCard> where TCard : ICard
    {
        public StringBoolean? Loading => Component.Loading;

        public RenderFragment? ProgressContent => Component.ProgressContent;
    }
}
