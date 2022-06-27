namespace BlazorComponent
{
    public static class ComponentExtensions
    {
        public static IDictionary<string, object> ToParameters<T>(this T t)
        {
            if (t == null)
                return default;

            if (t is not ComponentBase)
                return default;

            var finds = t.GetType().GetProperties().Where(p => p.CanWrite && p.CanRead && p.CustomAttributes.Any(attr => attr.AttributeType == typeof(ParameterAttribute))).ToArray();
            if (finds == null)
                return default;
            var result = new Dictionary<string, object>();
            foreach (var p in finds)
            {
                var value = p.GetValue(t);
                if (value == null)
                    continue;
                result.Add(p.Name, value);
            }
            if (result.Count == 0)
                return default;

            return result;
        }
    }
}
