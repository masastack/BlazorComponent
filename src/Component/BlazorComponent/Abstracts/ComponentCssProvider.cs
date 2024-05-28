namespace BlazorComponent.Abstracts
{
    public class ComponentCssProvider
    {
        private readonly Dictionary<string, Action<CssBuilder>> _cssConfig = new();
        private readonly Dictionary<string, Action<StyleBuilder>> _styleConfig = new();

        private Func<string?> StaticClass { get; }

        private Func<string?> StaticStyle { get; }

        public ComponentCssProvider(Func<string?> staticClass, Func<string?> staticStyle)
        {
            StaticClass = staticClass;
            StaticStyle = staticStyle;
        }
        
        /// <summary>
        /// Apply css to default element
        /// </summary>
        /// <param name="cssAction"></param>
        /// <param name="styleAction"></param>
        /// <returns></returns>
        public ComponentCssProvider Apply(Action<CssBuilder>? cssAction = null, Action<StyleBuilder>? styleAction = null)
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
        public ComponentCssProvider Apply(string name, Action<CssBuilder>? cssAction = null, Action<StyleBuilder>? styleAction = null)
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
        public ComponentCssProvider Merge(Action<CssBuilder>? mergeCssAction = null, Action<StyleBuilder>? mergeStyleAction = null)
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
        public ComponentCssProvider Merge(string name, Action<CssBuilder>? mergeCssAction = null, Action<StyleBuilder>? mergeStyleAction = null)
        {
            if (mergeCssAction != null)
            {
                var cssAction = _cssConfig.GetValueOrDefault(name);
                _cssConfig[name] = cssBuilder =>
                {
                    cssAction?.Invoke(cssBuilder);
                    mergeCssAction(cssBuilder);
                };
            }

            if (mergeStyleAction != null)
            {
                var styleAction = _styleConfig.GetValueOrDefault(name);
                _styleConfig[name] = styleBuilder =>
                {
                    styleAction?.Invoke(styleBuilder);
                    mergeStyleAction(styleBuilder);
                };
            }

            return this;
        }

        /// <summary>
        /// remove css 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ComponentCssProvider Remove(string name)
        {
            _cssConfig.Remove(name);
            _styleConfig.Remove(name);

            return this;
        }

        /// <summary>
        /// Get class of default element
        /// </summary>
        /// <param name="addDefaultCssImplicitly"></param>
        /// <returns></returns>
        public string? GetClass(bool addDefaultCssImplicitly = true)
        {
            return GetClass("default", addDefaultCssImplicitly: addDefaultCssImplicitly);
        }

        /// <summary>
        /// Get style of default element
        /// </summary>
        /// <returns></returns>
        public string? GetStyle() => GetStyle("default");

        /// <summary>
        /// Get class of named element
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="addDefaultCssImplicitly"></param>
        /// <returns></returns>
        public string? GetClass(string name, object? data = null, bool addDefaultCssImplicitly = true)
        {
            var action = _cssConfig.GetValueOrDefault(name);

            var builder = new CssBuilder
            {
                Data = data
            };

            action?.Invoke(builder);

            if (name == "default" && addDefaultCssImplicitly)
            {
                builder.Add(StaticClass);
            }

            return builder.Class;
        }

        /// <summary>
        /// Get style of named element
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string? GetStyle(string name, object? data = null)
        {
            var action = _styleConfig.GetValueOrDefault(name);

            var builder = new StyleBuilder
            {
                Data = data
            };

            action?.Invoke(builder);

            if (name == "default")
            {
                builder.Add(StaticStyle);
            }

            return builder.Style;
        }
    }
}
