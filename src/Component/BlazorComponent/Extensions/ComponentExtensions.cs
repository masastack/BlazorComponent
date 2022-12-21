namespace BlazorComponent
{
    public static class ComponentExtensions
    {
        public static IDictionary<string, object> ToParameters<T>(this T t)
        {
            var result = new Dictionary<string, object>();
            if (t != null)
            {
                var finds = t.GetType().GetProperties().Where(p => p.CanWrite && p.CanRead && p.CustomAttributes.Any(attr => attr.AttributeType == typeof(ParameterAttribute))).ToArray();
                foreach (var p in finds)
                {
                    var value = p.GetValue(t);
                    if (value == null)
                        continue;
                    result.Add(p.Name, value);
                }
            }
            return result;
        }
    }
}
