using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ComponentAbstractBase<TComponent> : ComponentBase where TComponent : IHasProviderComponent
    {
        [CascadingParameter]
        public IHasProviderComponent HasProviderComponent { get; set; }

        public TComponent Component => (TComponent)HasProviderComponent;

        public ComponentCssProvider CssProvider => Component.CssProvider;

        public ComponentAbstractProvider AbstractProvider => Component.AbstractProvider;

        protected override void OnParametersSet()
        {
            if (HasProviderComponent == null || HasProviderComponent is not TComponent)
            {
                throw new ArgumentException(nameof(HasProviderComponent));
            }
        }
    }
}
