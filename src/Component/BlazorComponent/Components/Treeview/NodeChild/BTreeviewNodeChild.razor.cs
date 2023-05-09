namespace BlazorComponent
{
    public partial class BTreeviewNodeChild<TItem, TKey, TComponent> where TComponent : IHasProviderComponent
    {
        [Parameter, EditorRequired]
        public TItem Item { get; set; } = default!;

        [Parameter]
        public bool ParentIsDisabled { get; set; }
    }
}
