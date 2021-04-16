using BlazorComponent.Components.Core.CssProcess;
using System.Linq;

namespace BlazorComponent
{
    public class CssBuilder : BuilderBase
    {
        public string Class => ToString();

        internal string OriginalClass { get; set; }

        public override string ToString()
        {
            return string.Join(" ", _mapper.Where(i => i.Value()).Select(i => i.Key()));
        }

        public override BuilderBase Clear()
        {
            _mapper.Add(() => OriginalClass, () => !string.IsNullOrEmpty(OriginalClass));

            return base.Clear();
        }
    }
}
