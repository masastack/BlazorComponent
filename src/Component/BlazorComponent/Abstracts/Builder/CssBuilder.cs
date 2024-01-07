namespace BlazorComponent
{
    public class CssBuilder : BuilderBase
    {
        protected virtual List<string?> GetClassNames()
        {
            return Mapper
                   .Where(kv => kv.Value())
                   .Select(kv => kv.Key()?.Trim())
                   .Where(css => !string.IsNullOrWhiteSpace(css))
                   .ToList();
        }

        public string? Class => GetClass();

        public string? GetClass()
        {
            var classList = GetClassNames();

            if (classList.Count == 0)
            {
                //In this case,style will never render as class="" but nothing
                return null;
            }

            return string.Join(" ", classList);
        }
    }
}
