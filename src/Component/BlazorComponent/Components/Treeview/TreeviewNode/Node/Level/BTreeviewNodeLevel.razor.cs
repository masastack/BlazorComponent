using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTreeviewNodeLevel<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        [Parameter]
        public int Level { get; set; }
    }
}
