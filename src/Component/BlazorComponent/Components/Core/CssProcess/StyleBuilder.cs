using System.Linq;
using BlazorComponent.Components.Core.CssProcess;

namespace BlazorComponent
{
    public class StyleBuilder : BuilderBase
    {
        public string Style => ToString();

        internal string OriginalStyle { get; set; }

        public override string ToString()
        {
            var separator = "; ";
            var style = string.Join(separator, _mapper.Where(i => i.Value()).Select(i => i.Key()));

            if (!string.IsNullOrWhiteSpace(style) && style.StartsWith(separator))
            {
                style = style[separator.Length..];
            }

            return style;
        }

        public override BuilderBase Clear()
        {
            _mapper.Add(() => OriginalStyle, () => !string.IsNullOrEmpty(OriginalStyle));

            return base.Clear();
        }
    }
}
