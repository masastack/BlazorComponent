using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTreeviewNodePrependSlot<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public TItem Item => Component.Item;

        public RenderFragment<TItem> PrependContent => Component.PrependContent;
    }
}
