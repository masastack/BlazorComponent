using System.Linq;
using BlazorComponent.Components.Core.CssProcess;

namespace BlazorComponent
{
    public class StyleBuilder : BuilderBase
    {
        public string Style => _mapper.GetStyle();
    }
}
