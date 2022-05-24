namespace BlazorComponent
{
    public class BDragItem<T>
    {
        public bool Enable { get; set; } = true;

        public bool Fixed { get; set; } = false;

        public string CssClass { get; set; } = string.Empty;

        public string CssStyle { get; set; } = string.Empty;

        public T Item { get; set; }
    }
}