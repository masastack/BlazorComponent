namespace BlazorComponent
{
    public partial class BTreeviewNodeCheckbox<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public string ComputedIcon => Component.ComputedIcon;

        public string Name => "checkbox";
    }
}
