namespace BlazorComponent
{
    public class BDragDropService
    {
        public BDragItem? DragItem { get; set; }

        public void Reset()
        {
            DragItem = default;
        }
    }
}
