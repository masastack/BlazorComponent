namespace BlazorComponent
{
    public interface ILoadable : IHasProviderComponent
    {
        public StringBoolean? Loading { get; }

        public RenderFragment? ProgressContent { get; }
    }
}
