namespace BlazorComponent
{
    public partial class BFileInputChips<TValue, TInput> where TInput : IFileInput<TValue>
    {
        public bool IsDirty => Component.IsDirty;

        public IList<string> Text => Component.Text;
    }
}
