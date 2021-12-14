using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTreeviewNodeToggle<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public string Name => "toggle";

        public bool IsLoading => Component.IsLoading;

        public string LoadingIcon => Component.LoadingIcon;

        public string ExpandIcon => Component.ExpandIcon;
    }
}
