namespace BlazorComponent
{
    public static class BuilderBaseExtensions
    {
        public static TBuilder Add<TBuilder>(this TBuilder builder, string? name)
            where TBuilder : BuilderBase
        {
            if (name != null)
            {
                builder._mapper.TryAdd(() => name, () => true);
            }

            return builder;
        }

        public static TBuilder Add<TBuilder>(this TBuilder builder, Func<string> funcName)
            where TBuilder : BuilderBase
        {
            builder._mapper.TryAdd(funcName, () => true);
            return builder;
        }

        public static TBuilder AddIf<TBuilder>(this TBuilder builder, Func<string> funcName, Func<bool> func)
            where TBuilder : BuilderBase
        {
            builder._mapper.TryAdd(funcName, func);
            return builder;
        }

        public static TBuilder AddIf<TBuilder>(this TBuilder builder, string? name, Func<bool> func)
            where TBuilder : BuilderBase
        {
            builder._mapper.TryAdd(() => name, func);
            return builder;
        }

        public static TBuilder AddFirstIf<TBuilder>(this TBuilder builder, params(Func<string> funcName, Func<bool> func)[] list)
            where TBuilder : BuilderBase
        {
            var item = list.FirstOrDefault(u => u.func.Invoke());

            if (!item.Equals(default))
            {
                builder._mapper.TryAdd(item.funcName, item.func);
            }

            return builder;
        }

        public static TBuilder AddFirstIf<TBuilder>(this TBuilder builder, params(string name, Func<bool> func)[] list)
            where TBuilder : BuilderBase
        {
            var item = list.FirstOrDefault(u => u.func.Invoke());

            if (!item.Equals(default))
            {
                builder._mapper.TryAdd(() => item.name, item.func);
            }

            return builder;
        }

        public static TBuilder Clear<TBuilder>(this TBuilder builder)
            where TBuilder : BuilderBase
        {
            builder._mapper.Clear();
            return builder;
        }

        public static string? GetClass(this Dictionary<Func<string>, Func<bool>> mapper)
        {
            var classList = mapper.Where(i => i.Value() && !string.IsNullOrWhiteSpace(i.Key())).Select(i => i.Key()?.Trim());
            if (!classList.Any())
            {
                //In this case,style will never render as class="" but nothing
                return null;
            }

            return string.Join(" ", classList);
        }

        public static string GetStyle(this Dictionary<Func<string>, Func<bool>> mapper)
        {
            var styleList = mapper.Where(i => i.Value() && !string.IsNullOrWhiteSpace(i.Key())).Select(i => i.Key()?.Trim().Trim(';'));
            if (!styleList.Any())
            {
                //In this case,style will never render as style="" but nothing
                return null;
            }

            return string.Join(";", styleList);
        }
    }
}
