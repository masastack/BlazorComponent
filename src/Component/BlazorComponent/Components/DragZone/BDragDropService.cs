namespace BlazorComponent
{
    public class BDragDropService<T>
    {
        public T DragItem { get; set; }

        public void Reset()
        {
            DragItem = default;
        }
    }
}
