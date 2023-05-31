namespace BlazorComponent
{
    public static class BuilderBaseExtensions
    {
        public static TBuilder Add<TBuilder>(this TBuilder builder, string? name)
            where TBuilder : BuilderBase
        {
            if (name != null)
            {
                builder.Mapper.TryAdd(() => name, () => true);
            }

            return builder;
        }

        public static TBuilder Add<TBuilder>(this TBuilder builder, Func<string?> funcName)
            where TBuilder : BuilderBase
        {
            builder.Mapper.TryAdd(funcName, () => true);
            return builder;
        }

        public static TBuilder AddIf<TBuilder>(this TBuilder builder, Func<string?> funcName, Func<bool> func)
            where TBuilder : BuilderBase
        {
            builder.Mapper.TryAdd(funcName, func);
            return builder;
        }

        public static TBuilder AddIf<TBuilder>(this TBuilder builder, string? name, Func<bool> func)
            where TBuilder : BuilderBase
        {
            builder.Mapper.TryAdd(() => name, func);
            return builder;
        }

        public static TBuilder AddFirstIf<TBuilder>(this TBuilder builder, params(Func<string> funcName, Func<bool> func)[] list)
            where TBuilder : BuilderBase
        {
            var item = list.FirstOrDefault(u => u.func.Invoke());

            if (!item.Equals(default))
            {
                builder.Mapper.TryAdd(item.funcName, item.func);
            }

            return builder;
        }

        public static TBuilder AddFirstIf<TBuilder>(this TBuilder builder, params(string name, Func<bool> func)[] list)
            where TBuilder : BuilderBase
        {
            var item = list.FirstOrDefault(u => u.func.Invoke());

            if (!item.Equals(default))
            {
                builder.Mapper.TryAdd(() => item.name, item.func);
            }

            return builder;
        }

        public static TBuilder Clear<TBuilder>(this TBuilder builder)
            where TBuilder : BuilderBase
        {
            builder.Mapper.Clear();
            return builder;
        }
    }
}
