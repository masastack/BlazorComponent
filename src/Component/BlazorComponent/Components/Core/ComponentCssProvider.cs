using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Components.Core;
using BlazorComponent.Components.Core.CssProcess;

namespace BlazorComponent
{
    public class ComponentCssProvider
    {
        private readonly Dictionary<ComponentKey, Action<CssBuilder>> _cssConfig = new();
        private readonly Dictionary<ComponentKey, Action<StyleBuilder>> _styleConfig = new();

        internal Func<string> StaticClassProvider { get; set; }

        internal Func<string> StaticStyleProvider { get; set; }

        public ComponentCssProvider Apply(Type type, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            if (cssAction != null)
            {
                _cssConfig.TryAdd(new ComponentKey(type), cssAction);
            }

            if (styleAction != null)
            {
                _styleConfig.TryAdd(new ComponentKey(type), styleAction);
            }

            return this;
        }

        public ComponentCssProvider Apply(Type type, string name, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            if (cssAction != null)
            {
                _cssConfig.TryAdd(new ComponentKey(type, name), cssAction);
            }

            if (styleAction != null)
            {
                _styleConfig.TryAdd(new ComponentKey(type, name), styleAction);
            }

            return this;
        }

        public ComponentCssProvider Apply<TComponent>(Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Apply(typeof(TComponent), cssAction, styleAction);
        }

        public ComponentCssProvider Apply<TComponent>(string name, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Apply(typeof(TComponent), name, cssAction, styleAction);
        }

        public ComponentCssProvider Merge<TComponent>(Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
        {
            var key = ComponentKey.Get<TComponent>();
            Merge(key, mergeCssAction, mergeStyleAction);

            return this;
        }

        private void Merge(ComponentKey key, Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
        {
            if (mergeCssAction != null)
            {
                var cssAction = _cssConfig.GetValueOrDefault(key);
                _cssConfig[key] = cssBuilder =>
                {
                    cssAction?.Invoke(cssBuilder);
                    mergeCssAction?.Invoke(cssBuilder);
                };
            }

            if (mergeStyleAction != null)
            {
                var styleAction = _styleConfig.GetValueOrDefault(key);
                _styleConfig[key] = styleBuilder =>
                {
                    styleAction?.Invoke(styleBuilder);
                    mergeStyleAction?.Invoke(styleBuilder);
                };
            }
        }

        public ComponentCssProvider Merge<TComponent>(string name, Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
        {
            var key = ComponentKey.Get<TComponent>(name);
            Merge(key, mergeCssAction, mergeStyleAction);

            return this;
        }

        public string GetClass(Type type)
        {
            var action = _cssConfig.GetValueOrDefault(new ComponentKey(type), _ => { });

            var builder = new CssBuilder();
            action?.Invoke(builder);

            builder.Add(StaticClassProvider);

            return builder.Class;
        }

        public string GetStyle(Type type)
        {
            var action = _styleConfig.GetValueOrDefault(new ComponentKey(type), _ => { });

            var builder = new StyleBuilder();
            action?.Invoke(builder);

            builder.Add(StaticStyleProvider);

            return builder.Style;
        }

        public string GetClass(Type type, string name)
        {
            var action = _cssConfig.GetValueOrDefault(new ComponentKey(type, name), _ => { });

            var builder = new CssBuilder();
            action?.Invoke(builder);

            return builder.Class;
        }

        public string GetStyle(Type type, string name)
        {
            var action = _styleConfig.GetValueOrDefault(new ComponentKey(type, name), _ => { });

            var builder = new StyleBuilder();
            action?.Invoke(builder);

            return builder.Style;
        }

        public string GetClass<TComponent>(TComponent component)
        {
            return GetClass(typeof(TComponent));
        }

        public string GetStyle<TComponent>(TComponent component)
        {
            return GetStyle(typeof(TComponent));
        }

        public string GetClass<TComponent>(TComponent component, string name)
        {
            return GetClass(typeof(TComponent), name);
        }

        public string GetStyle<TComponent>(TComponent component, string name)
        {
            return GetStyle(typeof(TComponent), name);
        }

        public ComponentCssProvider<TComponent> AsProvider<TComponent>()
        {
            var provider = new ComponentCssProvider<TComponent>(this);
            return provider;
        }
    }
}
