namespace BlazorComponent
{
    public static class ThemeCssBuilder
    {
        public static string Build(Palette theme)
        {
            var combinePrefix = theme.CombinePrefix;
            combinePrefix ??= string.Empty;
            combinePrefix = combinePrefix.EndsWith(' ') ? combinePrefix : $"{combinePrefix} ";

            var lstCss = new List<string>()
            {
                $":root{{--masa-palette-primary:{theme.Primary};",
                $"--masa-palette-secondary:{theme.Secondary};",
                $"--masa-palette-accent:{theme.Accent};",
                $"--masa-palette-error:{theme.Error};",
                $"--masa-palette-info:{theme.Info};",
                $"--masa-palette-success:{theme.Success};",
                $"--masa-palette-warning:{theme.Warning};}}",
            };

            theme.UserDefined?.ForEach(kvp => { lstCss.Add(Build(combinePrefix, kvp.Key.ToLowerInvariant(), kvp.Value)); });

            return string.Concat(lstCss);
        }

        private static string Build(string combinePrefix, string selector, string? color)
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
