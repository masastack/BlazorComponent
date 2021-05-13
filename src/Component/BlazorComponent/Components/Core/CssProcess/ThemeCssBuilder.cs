using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ThemeCssBuilder
    {
        public string Build()
        {
            var combinePrefix = Variables.Theme.CombinePrefix;
            combinePrefix ??= string.Empty;
            combinePrefix = combinePrefix.EndsWith(' ') ? combinePrefix : $"{combinePrefix} ";

            if (Variables.Theme == null) return string.Empty;

            var lstCss = new List<string>()
            {
                $"{combinePrefix}a {{ color: { Variables.Theme.Primary }; }}",
                Build(combinePrefix, nameof(Variables.Theme.Primary).ToLowerInvariant(), Variables.Theme.Primary),
                Build(combinePrefix, nameof(Variables.Theme.Secondary).ToLowerInvariant(), Variables.Theme.Secondary),
                Build(combinePrefix, nameof(Variables.Theme.Accent).ToLowerInvariant(), Variables.Theme.Accent),
                Build(combinePrefix, nameof(Variables.Theme.Error).ToLowerInvariant(), Variables.Theme.Error),
                Build(combinePrefix, nameof(Variables.Theme.Info).ToLowerInvariant(), Variables.Theme.Info),
                Build(combinePrefix, nameof(Variables.Theme.Success).ToLowerInvariant(), Variables.Theme.Success),
                Build(combinePrefix, nameof(Variables.Theme.Warning).ToLowerInvariant(), Variables.Theme.Warning),
            };

            Variables.Theme.UserDefined?.ForEach(kvp =>
            {
                lstCss.Add(Build(combinePrefix, kvp.Key.ToLowerInvariant(), kvp.Value));
            });

            return $"<style>{string.Concat(lstCss)}</style>";
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
