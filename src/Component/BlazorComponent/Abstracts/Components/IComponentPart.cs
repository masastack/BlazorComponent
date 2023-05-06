namespace BlazorComponent
{
    public interface IComponentPart
    {
        RenderFragment Content { get; }

        void Attach(IHasProviderComponent component);

        void SetParameters(ParameterView parameterView);
    }
}
