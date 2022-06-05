namespace BlazorComponent
{
    public class BDragDropService
    {
        public BDragItem DragItem { get; set; }

        public bool IsClone { get; set; }

        public void Reset()
        {
            DragItem = default;
            IsClone= false;
        }
    }
}
