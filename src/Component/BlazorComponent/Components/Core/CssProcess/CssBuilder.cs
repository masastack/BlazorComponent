using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class CssBuilder
    {
        public string Class => ToString();

        internal string OriginalClass { get; set; }

        public override string ToString()
        {
            return string.Join(" ", _mapper.Where(i => i.Value()).Select(i => i.Key()));
        }

        private readonly Dictionary<Func<string>, Func<bool>> _mapper = new Dictionary<Func<string>, Func<bool>>();

        public CssBuilder Add(string name)
        {
            _mapper.Add(() => name, () => true);
            return this;
        }

        public CssBuilder Add(Func<string> funcName)
        {
            _mapper.Add(funcName, () => true);
            return this;
        }

        public CssBuilder AddIf(Func<string> funcName, Func<bool> func)
        {
            _mapper.Add(funcName, func);
            return this;
        }

        public CssBuilder AddIf(string name, Func<bool> func)
        {
            _mapper.Add(() => name, func);
            return this;
        }

        public CssBuilder Clear()
        {
            _mapper.Clear();

            _mapper.Add(() => OriginalClass, () => !string.IsNullOrEmpty(OriginalClass));

            return this;
        }
    }
}
