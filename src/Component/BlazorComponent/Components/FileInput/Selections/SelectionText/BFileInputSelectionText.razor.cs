namespace BlazorComponent
{
    public partial class BFileInputSelectionText<TValue, TInput> where TInput : IFileInput<TValue>
    {
        public IList<string> Text => Component.Text;

        public bool ShowSize => Component.ShowSize;

        public StringNumberBoolean Counter => Component.Counter;

        public StringNumber ComputedCounterValue => Component.ComputedCounterValue;
    }
}
