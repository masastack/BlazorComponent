namespace BlazorComponent
{
    public class StyleBuilder : BuilderBase
    {
        public string? Style => GetStyle();

        public string? GetStyle()
        {
            var styleList = Mapper
                            .Where(i => i.Value() && !string.IsNullOrWhiteSpace(i.Key()))
                            .Select(i => i.Key()?.Trim().Trim(';'))
                            .ToList();

            if (!styleList.Any())
            {
                //In this case,style will never render as style="" but nothing
                return null;
            }

            return string.Join(";", styleList) + ";";
        }
    }
}
