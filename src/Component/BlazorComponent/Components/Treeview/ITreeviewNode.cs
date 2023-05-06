namespace BlazorComponent
{
    public interface ITreeviewNode<TItem, TKey> : IHasProviderComponent
    {
        RenderFragment<TreeviewItem<TItem>> PrependContent { get; }

        RenderFragment<TreeviewItem<TItem>> LabelContent { get; }

        string Text { get; }

        RenderFragment<TreeviewItem<TItem>> AppendContent { get; }

        bool Selectable { get; }

        string ComputedIcon { get; }

        bool HasChildren { get; }

        bool IsLoading { get; }

        string LoadingIcon { get; }

        string ExpandIcon { get; }

        int Level { get; }

        bool IsOpen { get; }

        List<TItem> ComputedChildren { get; }

        bool Disabled { get; }

        Task HandleOnClick(MouseEventArgs args);

        TKey Key { get; }

        bool IsLeaf { get; }

        bool IsActive { get; }

        bool IsSelected { get; }

        bool IsIndeterminate { get; }

        TItem Item { get; }
    }
}