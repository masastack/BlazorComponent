namespace BlazorComponent
{
    public partial class BProgressCircularCircle<TProgressCircular> where TProgressCircular : IProgressCircular
    {
        [Parameter]
        public string Name { get; set; } = null!;

        [Parameter]
        public string Offset { get; set; } = null!;

        public Dictionary<string, object?> CircleAttrs => Component.CircleAttrs;
    }
}
