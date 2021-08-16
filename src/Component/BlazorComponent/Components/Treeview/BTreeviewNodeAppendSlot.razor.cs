using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTreeviewNodeAppendSlot<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public RenderFragment AppendContent => Component.AppendContent;
    }
}
