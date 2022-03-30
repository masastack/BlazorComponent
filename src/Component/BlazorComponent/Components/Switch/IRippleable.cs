namespace BlazorComponent
{
    public interface IRippleable : IHasProviderComponent
    {
        bool? Ripple { get; }
    }
}
