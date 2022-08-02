namespace BlazorComponent
{
    public partial class BTreeviewNodeChild<TItem, TKey, TComponent> where TComponent : IHasProviderComponent
    {
        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public bool ParentIsDisabled { get; set; }
    }
}
