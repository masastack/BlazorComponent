using System.Linq;
using BlazorComponent.Components.Core.CssProcess;

namespace BlazorComponent
{
    public class StyleBuilder : BuilderBase
    {
        public string Style => ToString();

        public override string ToString()
        {
            var styleList = _mapper.Where(i => i.Value() && !string.IsNullOrWhiteSpace(i.Key())).Select(i => i.Key()?.Trim().Trim(';'));
            if (!styleList.Any())
            {
                //In this case,style will never render as style="" but nothing
                return null;
            }

            return string.Join(";", styleList);
        }
    }
}
