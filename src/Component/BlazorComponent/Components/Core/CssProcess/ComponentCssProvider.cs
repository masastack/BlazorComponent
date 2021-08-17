using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Components.Core;
using BlazorComponent.Components.Core.CssProcess;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public class ComponentCssProvider
    {
        private readonly Dictionary<ComponentKey, Action<CssBuilder>> _cssConfig = new();
        private readonly Dictionary<ComponentKey, Action<StyleBuilder>> _styleConfig = new();
        private Type _defaultType;

        internal Func<string> StaticClassProvider { get; set; }

        internal Func<string> StaticStyleProvider { get; set; }

        /// <summary>
        /// The default component type
        /// </summary>
        public Type DefaultType => _defaultType ?? typeof(ComponentBase);

        /// <summary>
        /// Apply css to given component
        /// <para>
        /// The default component will be set if not
        /// </para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cssAction"></param>
        /// <param name="styleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Apply(Type type, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Apply(new ComponentKey(type), cssAction, styleAction);
        }

        private ComponentCssProvider Apply(ComponentKey key, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            if (_defaultType == null)
            {
                _defaultType = key.Type;
            }

            if (cssAction != null)
            {
                _cssConfig.TryAdd(key, cssAction);
            }

            if (styleAction != null)
            {
                _styleConfig.TryAdd(key, styleAction);
            }

            return this;
        }

        /// <summary>
        /// Apply css to a element of given component
        /// <para>
        /// The default component will be set if not
        /// </para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="cssAction"></param>
        /// <param name="styleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Apply(Type type, string name, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Apply(new ComponentKey(type, name), cssAction, styleAction);
        }

        /// <summary>
        /// Apply css to default component
        /// </summary>
        /// <param name="cssAction"></param>
        /// <param name="styleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Apply(Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Apply(DefaultType, cssAction, styleAction);
        }

        /// <summary>
        /// Apply css to a element of default component
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cssAction"></param>
        /// <param name="styleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Apply(string name, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Apply(DefaultType, name, cssAction, styleAction);
        }

        /// <summary>
        /// Apply css to given component
        /// <para>
        /// The default component will be set if not
        /// </para>
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="cssAction"></param>
        /// <param name="styleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Apply<TComponent>(Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Apply(typeof(TComponent), cssAction, styleAction);
        }

        /// <summary>
        /// Apply css to a element of given component
        /// <para>
        /// The default component will be set if not
        /// </para>
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="name"></param>
        /// <param name="cssAction"></param>
        /// <param name="styleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Apply<TComponent>(string name, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Apply(typeof(TComponent), name, cssAction, styleAction);
        }

        /// <summary>
        /// Merge css to given component
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="mergeCssAction"></param>
        /// <param name="mergeStyleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Merge<TComponent>(Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
        {
            var key = ComponentKey.Get<TComponent>();
            return Merge(key, mergeCssAction, mergeStyleAction);
        }

        private ComponentCssProvider Merge(ComponentKey key, Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
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

            return this;
        }

        /// <summary>
        /// Merge css to a element of given component
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="name"></param>
        /// <param name="mergeCssAction"></param>
        /// <param name="mergeStyleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Merge<TComponent>(string name, Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
        {
            var key = ComponentKey.Get<TComponent>(name);
            return Merge(key, mergeCssAction, mergeStyleAction);
        }

        /// <summary>
        /// Merge css to default component
        /// </summary>
        /// <param name="mergeCssAction"></param>
        /// <param name="mergeStyleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Merge(Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
        {
            var key = new ComponentKey(DefaultType);
            return Merge(key, mergeCssAction, mergeStyleAction);
        }

        /// <summary>
        /// Merge css to a element of default component
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mergeCssAction"></param>
        /// <param name="mergeStyleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Merge(string name, Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
        {
            var key = new ComponentKey(DefaultType, name);
            return Merge(key, mergeCssAction, mergeStyleAction);
        }

        /// <summary>
        /// Get class of given component
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetClass(Type type)
        {
            var action = _cssConfig.GetValueOrDefault(new ComponentKey(type), _ => { });

            var builder = new CssBuilder();
            action?.Invoke(builder);

            builder.Add(StaticClassProvider);
            return builder.Class;
        }

        /// <summary>
        /// Get style of given component
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetStyle(Type type)
        {
            var action = _styleConfig.GetValueOrDefault(new ComponentKey(type), _ => { });

            var builder = new StyleBuilder();
            action?.Invoke(builder);

            builder.Add(StaticStyleProvider);

            return builder.Style;
        }

        /// <summary>
        /// Get class of a element of given component
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetClass(Type type, string name)
        {
            var action = _cssConfig.GetValueOrDefault(new ComponentKey(type, name), _ => { });

            var builder = new CssBuilder();
            action?.Invoke(builder);

            return builder.Class;
        }

        /// <summary>
        /// Get style of a element of given component
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetStyle(Type type, string name)
        {
            var action = _styleConfig.GetValueOrDefault(new ComponentKey(type, name), _ => { });

            var builder = new StyleBuilder();
            action?.Invoke(builder);

            return builder.Style;
        }

        /// <summary>
        /// Get class of default component
        /// </summary>
        /// <returns></returns>
        public string GetClass()
        {
            return GetClass(DefaultType);
        }

        /// <summary>
        /// Get style of default component
        /// </summary>
        /// <returns></returns>
        public string GetStyle()
        {
            return GetStyle(DefaultType);
        }

        /// <summary>
        /// Get class of a element of default component
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetClass(string name)
        {
            return GetClass(DefaultType, name);
        }

        /// <summary>
        /// Get style of a element of default component
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetStyle(string name)
        {
            return GetStyle(DefaultType, name);
        }

        /// <summary>
        /// Get class of given component
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public string GetClass<TComponent>(TComponent component)
        {
            return GetClass(typeof(TComponent));
        }

        /// <summary>
        /// Get style of given component
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public string GetStyle<TComponent>(TComponent component)
        {
            return GetStyle(typeof(TComponent));
        }

        /// <summary>
        /// Get class of a element of given component
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="component"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetClass<TComponent>(TComponent component, string name)
        {
            return GetClass(typeof(TComponent), name);
        }

        /// <summary>
        /// Get style of a element of given component
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="component"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetStyle<TComponent>(TComponent component, string name)
        {
            return GetStyle(typeof(TComponent), name);
        }

        /// <summary>
        /// Set default component
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        public ComponentCssProvider AsProvider<TComponent>()
        {
            _defaultType = typeof(TComponent);
            return this;
        }
    }
}
