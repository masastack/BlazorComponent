using BlazorComponent.Abstracts;

namespace BlazorComponent
{
    public interface IHasProviderComponent : IComponent, IHandleAfterRender
    {
        ComponentCssProvider CssProvider { get; }

        ComponentAbstractProvider AbstractProvider { get; }
    }
}