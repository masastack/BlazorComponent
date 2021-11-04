using BlazorComponent.Components.Core.CssProcess;
using System.Linq;

namespace BlazorComponent
{
    public class CssBuilder : BuilderBase
    {
        public string Class => ToString();

        public override string ToString()
        {
            var classList = _mapper.Where(i => i.Value() && !string.IsNullOrWhiteSpace(i.Key())).Select(i => i.Key()?.Trim());
            if (!classList.Any())
            {
                //In this case,style will never render as class="" but nothing
                return null;
            }

            return string.Join(" ", classList);
        }
    }
}
