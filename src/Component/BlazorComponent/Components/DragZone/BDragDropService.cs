namespace BlazorComponent
{
    public class BDragDropService<T>
    {
        public BDragItem<T> DragItem { get; set; }

        public void Reset()
        {
            DragItem = default;
        }
    }
}
