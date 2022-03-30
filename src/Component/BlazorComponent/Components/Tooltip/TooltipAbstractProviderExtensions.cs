namespace BlazorComponent
{
    public static class TooltipAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyTooltipDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BTooltipContent<>), typeof(BTooltipContent<ITooltip>))
                .Apply(typeof(BTooltipActivator<>), typeof(BTooltipActivator<ITooltip>));
        }
    }
}
