using BlazorComponent.Components.Core.CssProcess;
using System.Linq;

namespace BlazorComponent
{
    public class CssBuilder : BuilderBase
    {
        public string Class => ToString();

        public override string ToString()
        {
            return string.Join(" ", _mapper.Where(i => i.Value()).Select(i => i.Key())).Trim();
        }
    }
}
