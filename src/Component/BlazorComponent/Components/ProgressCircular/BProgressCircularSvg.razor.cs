namespace BlazorComponent
{
    public partial class BProgressCircularSvg<TProgressCircular> where TProgressCircular : IProgressCircular
    {
        public Dictionary<string, object> SvgAttributes => Component.SvgAttributes;

        public bool Indeterminate => Component.Indeterminate;

        public string StrokeDashOffset => Component.StrokeDashOffset;
    }
}
