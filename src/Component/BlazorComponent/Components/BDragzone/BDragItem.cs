namespace BlazorComponent
{
    public class BDragItem<T>
    {
        public bool Enable { get; set; } = true;

        public bool Fixed { get; set; } = false;

        public string CssClass { get; set; } = string.Empty;

        public string CssStyle { get; set; } = string.Empty;

        public double X { get; set; }

        public double Y { get; set; }

        public T Item { get; set; }
    }
}
