namespace BlazorComponent
{
    public static class BChipAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyChipDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BChipContent<>), typeof(BChipContent<IChip>))
                .Apply(typeof(BChipClose<>), typeof(BChipClose<IChip>))
                .Apply(typeof(BChipFilter<>), typeof(BChipFilter<IChip>));
        }
    }
}
