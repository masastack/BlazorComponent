namespace BlazorComponent
{
    public class BDragDropService
    {
        public BDragItem DragItem { get; set; }

        public BDragZone Source { get; set; }

        public BDragZone Target { get; set; }

        public void Reset()
        {
            DragItem = default;
            Source= default;
            Target = default;
        }
    }
}
