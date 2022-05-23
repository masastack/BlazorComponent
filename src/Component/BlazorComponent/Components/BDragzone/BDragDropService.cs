namespace BlazorComponent
{
    internal class BDragDropService<T>
    {

        public BDragItem<T> DragItem { get; set; }       

        public BDragzone<T> DropRoot { get; set; }       

        public BDragItem<T> DropItem { get; set; }

        public void Reset()
        {
            DragItem = default;
            DropItem = default;
            DropRoot = default;
        }
    }
}
