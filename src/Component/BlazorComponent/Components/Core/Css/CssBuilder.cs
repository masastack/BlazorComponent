using BlazorComponent.Components.Core.CssProcess;
using System.Linq;

namespace BlazorComponent
{
    public class CssBuilder : BuilderBase
    {
        public string Class => _mapper.GetClass();
    }
}
