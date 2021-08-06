using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ComponentAbstractBase<TComponent> : ComponentBase where TComponent : IAbstractComponent
    {
        [CascadingParameter]
        public IAbstractComponent AbstractComponent { get; set; }

        public TComponent Component => (TComponent)AbstractComponent;

        public ComponentCssProvider CssProvider => Component.CssProvider;

        public ComponentAbstractProvider AbstractProvider => Component.AbstractProvider;

        protected override void OnParametersSet()
        {
            if (AbstractComponent == null || AbstractComponent is not TComponent)
            {
                throw new ArgumentException(nameof(AbstractComponent));
            }
        }
    }
}
