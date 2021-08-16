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
        public RenderFragment LabelContent => Component.LabelContent;

        public string Text => Component.Text;
    }
}
