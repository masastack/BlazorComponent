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
        private readonly Dictionary<string, Action<CssBuilder>> _cssConfig = new();
        private readonly Dictionary<string, Action<StyleBuilder>> _styleConfig = new();

        internal Func<string> StaticClassProvider { get; set; }

        internal Func<string> StaticStyleProvider { get; set; }

        /// <summary>
        /// Apply css to default element
        /// </summary>
        /// <param name="cssAction"></param>
        /// <param name="styleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Apply(Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            return Apply("default", cssAction, styleAction);
        }

        /// <summary>
        /// Apply css to named element
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cssAction"></param>
        /// <param name="styleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Apply(string name, Action<CssBuilder> cssAction = null, Action<StyleBuilder> styleAction = null)
        {
            if (cssAction != null)
            {
                _cssConfig.Add(name, cssAction);
            }

            if (styleAction != null)
            {
                _styleConfig.Add(name, styleAction);
            }

            return this;
        }

        /// <summary>
        /// Merge css to default element
        /// </summary>
        /// <param name="mergeCssAction"></param>
        /// <param name="mergeStyleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Merge(Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
        {
            return Merge("default", mergeCssAction, mergeStyleAction);
        }

        /// <summary>
        /// Merge css to default element
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mergeCssAction"></param>
        /// <param name="mergeStyleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Merge(string name, Action<CssBuilder> mergeCssAction = null, Action<StyleBuilder> mergeStyleAction = null)
        {
            if (mergeCssAction != null)
            {
                var cssAction = _cssConfig.GetValueOrDefault(name);
                _cssConfig[name] = cssBuilder =>
                {
                    cssAction?.Invoke(cssBuilder);
                    mergeCssAction?.Invoke(cssBuilder);
                };
            }

            if (mergeStyleAction != null)
            {
                var styleAction = _styleConfig.GetValueOrDefault(name);
                _styleConfig[name] = styleBuilder =>
                {
                    styleAction?.Invoke(styleBuilder);
                    mergeStyleAction?.Invoke(styleBuilder);
                };
            }

            return this;
        }

        /// <summary>
        /// Get class of default element
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetClass()
        {
            return GetClass("default");
        }

        /// <summary>
        /// Get style of default element
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetStyle()
        {
            return GetStyle("default");
        }

        /// <summary>
        /// Get class of named element
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetClass(string name = "default")
        {
            var action = _cssConfig.GetValueOrDefault(name);

            var builder = new CssBuilder();
            action?.Invoke(builder);

            if (name == "default")
            {
                builder.Add(StaticClassProvider);
            }

            return builder.Class;
        }

        /// <summary>
        /// Get style of named element
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetStyle(string name = "default")
        {
            var action = _styleConfig.GetValueOrDefault(name);

            var builder = new StyleBuilder();
            action?.Invoke(builder);

            if (name == "default")
            {
                builder.Add(StaticStyleProvider);
            }

            return builder.Style;
        }
    }
}
