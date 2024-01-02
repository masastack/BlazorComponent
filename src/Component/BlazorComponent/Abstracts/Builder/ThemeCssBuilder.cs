using System.Text;

namespace BlazorComponent
{
    public class ThemeCssBuilder
    {
        public string? Build(ThemeOptions theme, bool dark)
        {
            var combinePrefix = theme.CombinePrefix;
            combinePrefix ??= string.Empty;
            combinePrefix = combinePrefix.EndsWith(' ') ? combinePrefix : $"{combinePrefix} ";

            var lstCss = new List<string>()
            {
                $"{combinePrefix}a {{ color: {theme.Primary}; }}",
                Build(combinePrefix, nameof(theme.Primary).ToLowerInvariant(), theme.Primary, theme.OnPrimary),
                Build(combinePrefix, nameof(theme.Secondary).ToLowerInvariant(), theme.Secondary, theme.OnSecondary),
                Build(combinePrefix, nameof(theme.Accent).ToLowerInvariant(), theme.Accent, theme.OnAccent),
                Build(combinePrefix, nameof(theme.Error).ToLowerInvariant(), theme.Error, theme.OnError),
                Build(combinePrefix, nameof(theme.Info).ToLowerInvariant(), theme.Info),
                Build(combinePrefix, nameof(theme.Success).ToLowerInvariant(), theme.Success),
                Build(combinePrefix, nameof(theme.Warning).ToLowerInvariant(), theme.Warning),
                BuildSurface(combinePrefix, dark, theme.Surface, theme.OnSurface)
            };

            theme.UserDefined?.ForEach(kvp => { lstCss.Add(Build(combinePrefix, kvp.Key.ToLowerInvariant(), kvp.Value)); });

            return string.Concat(lstCss);
        }

        private string BuildSurface(string combinePrefix, bool isDark, string? background, string? color)
        {
            if (background is null && color is null)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($$"""

                                   {{(isDark ? ".theme--dark" : ".theme--light")}}{{combinePrefix}} {
                                   """);
            if (background is not null)
            {
                stringBuilder.Append($"""
                                      
                                         background: {background};
                                      """);
            }

            if (color is not null)
            {
                stringBuilder.Append($"""
                                      
                                         color: {color};
                                      """);
            }

            stringBuilder.Append("""

                                 }
                                 """);

            return stringBuilder.ToString();
        }

        private string Build(string combinePrefix, string selector, string color, string? onColor = null)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($$"""

                                   {{combinePrefix}}.{{selector}} {
                                       background-color: {{color}} !important;
                                       border-color: {{color}} !important;
                                   """);

            if (!string.IsNullOrWhiteSpace(onColor))
            {
                stringBuilder.Append($"""
                                      
                                          color: {onColor} !important;
                                      """);
            }

            stringBuilder.Append($$"""

                                   }
                                   {{combinePrefix}}.{{selector}}--text {
                                       color: {{color}} !important;
                                       caret-color: {{color}} !important;
                                   }
                                   """);

            return stringBuilder.ToString();
        }
    }
}
