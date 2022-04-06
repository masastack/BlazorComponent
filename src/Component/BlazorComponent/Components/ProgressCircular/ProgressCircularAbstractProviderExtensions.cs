namespace BlazorComponent
{
    public static class ProgressCircularAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyProgressCircularDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                 .Apply(typeof(BProgressCircularSvg<>), typeof(BProgressCircularSvg<IProgressCircular>))
                 .Apply(typeof(BProgressCircularInfo<>), typeof(BProgressCircularInfo<IProgressCircular>))
                 .Apply(typeof(BProgressCircularCircle<>), typeof(BProgressCircularCircle<IProgressCircular>));
        }
    }
}
