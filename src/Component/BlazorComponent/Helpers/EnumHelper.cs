using System.Collections.Concurrent;
using System.Reflection;

namespace BlazorComponent.Helpers
{
    public class EnumHelper
    {
        private static ConcurrentDictionary<string, Attribute> _dicAttribute = new ConcurrentDictionary<string, Attribute>();

        public static TAttribute GetAttribute<TAttribute>(Enum @enum)
            where TAttribute : Attribute
        {
            var type = @enum.GetType();
            var typeFullName = type.FullName;

            return _dicAttribute.GetOrAdd($"{typeFullName}_{@enum}", key => new Lazy<Attribute>(() =>
            {
                var attr = type.GetCustomAttribute<TAttribute>();

                return attr;
            }).Value) as TAttribute;
        }
    }
}
