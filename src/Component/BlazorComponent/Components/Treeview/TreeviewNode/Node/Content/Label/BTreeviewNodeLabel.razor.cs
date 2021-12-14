using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTreeviewNodeLabel<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public RenderFragment<TreeviewItem<TItem>> LabelContent => Component.LabelContent;

        public string Text => Component.Text;

        public RenderFragment ComputedLabelContent => LabelContent?.Invoke(new TreeviewItem<TItem>()
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