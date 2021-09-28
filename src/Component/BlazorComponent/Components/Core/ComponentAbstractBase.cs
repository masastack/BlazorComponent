using Microsoft.AspNetCore.Components;
using System;

namespace BlazorComponent
{
    public class ComponentAbstractBase<TComponent> : ComponentBase where TComponent : IHasProviderComponent
    {
        [CascadingParameter]
        public IHasProviderComponent HasProviderComponent { get; set; }
        
        [CascadingParameter]
        public TComponent Component { get; set; }

        public ComponentCssProvider CssProvider => Component.CssProvider;

        public ComponentAbstractProvider AbstractProvider => Component.AbstractProvider;

        protected override void OnParametersSet()
        {
            if (Component==null)
            {
                if (HasProviderComponent is TComponent component)
                {
                    Component = component;
                }
                else
                {
                    throw new ArgumentException(nameof(Component));
                }
                
            }
        }
    }
}