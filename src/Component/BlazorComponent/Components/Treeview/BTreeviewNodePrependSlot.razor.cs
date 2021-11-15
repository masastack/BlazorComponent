using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTreeviewNodePrependSlot<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public RenderFragment<TreeviewItem<TItem>> PrependContent => Component.PrependContent;

        public RenderFragment ComputedPrependContent => PrependContent?.Invoke(new TreeviewItem<TItem>
        {
            Item = Component.Item,
            Leaf = Component.IsLeaf,
            Selected = Component.IsSelected,
            Indeterminate = Component.IsIndeterminate,
            Active = Component.IsActive,
            Open = Component.IsOpen
        });
    }
}