using BlazorComponent.Abstracts;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IHasProviderComponent : IComponent, IHandleEvent, IHandleAfterRender
    {
        ComponentCssProvider CssProvider { get; }

        ComponentAbstractProvider AbstractProvider { get; }
    }
}
