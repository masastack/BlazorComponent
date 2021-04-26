using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ComponentCssProvider<TComponent>
    {
        private readonly ComponentCssProvider _inner;

        public ComponentCssProvider(ComponentCssProvider provider)
        {
            _inner = provider;
        }

        public ComponentCssProvider<TComponent> Apply(Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            _inner.Apply(typeof(TComponent), cssAction, styleAction);
            return this;
        }

        public ComponentCssProvider<TComponent> Apply(string name, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            _inner.Apply(typeof(TComponent), name, cssAction, styleAction);
            return this;
        }
    }
}
