using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTreeviewNodeAppendSlot<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public RenderFragment<TreeviewItem<TItem>> AppendContent => Component.AppendContent;

        public RenderFragment ComputedAppendContent => AppendContent?.Invoke(new TreeviewItem<TItem>()
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