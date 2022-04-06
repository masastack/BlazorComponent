namespace BlazorComponent
{
    public partial class BProgressLinearStream<TProgressLinear> where TProgressLinear : IProgressLinear
    {
        public bool Stream => Component.Stream;
    }
}
