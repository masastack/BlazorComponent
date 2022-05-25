namespace BlazorComponent
{
    public class BDragItem<T>
    {
        public string CssClass { get; set; } = string.Empty;

        public string CssStyle { get; set; } = string.Empty;

        public T Item { get; set; }
    }
}