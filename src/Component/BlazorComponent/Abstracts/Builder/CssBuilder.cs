namespace BlazorComponent
{
    public class CssBuilder : BuilderBase
    {
        public string? Class => GetClass();

        public string? GetClass()
        {
            var classList = Mapper
                            .Where(kv => kv.Value())
                            .Select(kv => kv.Key()?.Trim())
                            .Where(css => !string.IsNullOrWhiteSpace(css))
                            .ToList();

            if (!classList.Any())
            {
                //In this case,style will never render as class="" but nothing
                return null;
            }

            return string.Join(" ", classList);
        }
    }
}
