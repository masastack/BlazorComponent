namespace BlazorComponent
{
    public static class MainAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyMainDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BMainWrap<>), typeof(BMainWrap<IMain>));
        }
    }
}
