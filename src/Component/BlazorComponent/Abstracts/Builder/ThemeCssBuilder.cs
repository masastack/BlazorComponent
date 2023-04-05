namespace BlazorComponent
{
    public class ThemeCssBuilder
    {
        public string Build(ThemeOptions theme)
        {
            var combinePrefix = theme.CombinePrefix;
            combinePrefix ??= string.Empty;
            combinePrefix = combinePrefix.EndsWith(' ') ? combinePrefix : $"{combinePrefix} ";

            var lstCss = new List<string>()
            {
                $"{combinePrefix}a {{ color: {theme.Primary}; }}",
                Build(combinePrefix, nameof(theme.Primary).ToLowerInvariant(), theme.Primary),
                Build(combinePrefix, nameof(theme.Secondary).ToLowerInvariant(), theme.Secondary),
                Build(combinePrefix, nameof(theme.Accent).ToLowerInvariant(), theme.Accent),
                Build(combinePrefix, nameof(theme.Error).ToLowerInvariant(), theme.Error),
                Build(combinePrefix, nameof(theme.Info).ToLowerInvariant(), theme.Info),
                Build(combinePrefix, nameof(theme.Success).ToLowerInvariant(), theme.Success),
                Build(combinePrefix, nameof(theme.Warning).ToLowerInvariant(), theme.Warning),
            };

            theme.UserDefined?.ForEach(kvp => { lstCss.Add(Build(combinePrefix, kvp.Key.ToLowerInvariant(), kvp.Value)); });

            return string.Concat(lstCss);
        }

        private string Build(string combinePrefix, string selector, string color)
        {
            return @$"
                {combinePrefix}.{selector} {{
                    background-color: {color} !important;
                    border-color: {color} !important;
                }}
                {combinePrefix}.{selector}--text {{
                    color: {color} !important;
                    caret-color: {color} !important;
                }}";
        }
    }
}
