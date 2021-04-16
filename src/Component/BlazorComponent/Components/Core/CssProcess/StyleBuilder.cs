using System.Linq;

namespace BlazorComponent.Components.Core.CssProcess
{
    public class StyleBuilder : BuilderBase
    {
        public string Style => ToString();

        internal string OriginalStyle { get; set; }

        public override string ToString()
        {
            var style = string.Join(";", _mapper.Where(i => i.Value()).Select(i => i.Key()));

            if (!string.IsNullOrWhiteSpace(style))
            {
                style = style.Substring(1);
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
