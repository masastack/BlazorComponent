namespace BlazorComponent
{
    public partial class BProgressLinearContent<TProgressLinear> where TProgressLinear : IProgressLinear
    {
        public double Value => Component.Value;

        public RenderFragment<double>? ComponentChildContent => Component.ChildContent;
    }
}
