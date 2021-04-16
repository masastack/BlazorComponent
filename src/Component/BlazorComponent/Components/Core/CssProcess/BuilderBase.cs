using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorComponent.Components.Core.CssProcess
{
    public abstract class BuilderBase
    {
        internal readonly Dictionary<Func<string>, Func<bool>> _mapper = new();

        public BuilderBase Add(string name)
        {
            _mapper.Add(() => name, () => true);
            return this;
        }

        public BuilderBase Add(Func<string> funcName)
        {
            _mapper.Add(funcName, () => true);
            return this;
        }

        public BuilderBase AddIf(Func<string> funcName, Func<bool> func)
        {
            _mapper.Add(funcName, func);
            return this;
        }

        public BuilderBase AddIf(string name, Func<bool> func)
        {
            _mapper.Add(() => name, func);
            return this;
        }

        public BuilderBase AddFirstIf(params (Func<string> funcName, Func<bool> func)[] list)
        {
            var item = list.LastOrDefault(u => u.func.Invoke());

            if (!item.Equals(default))
            {
                _mapper.Add(item.funcName, item.func);
            }

            return this;
        }

        public BuilderBase AddFirstIf(params (string name, Func<bool> func)[] list)
        {
            var item = list.LastOrDefault(u => u.func.Invoke());

            if (!item.Equals(default))
            {
                _mapper.Add(() => item.name, item.func);
            }

            return this;
        }

        public virtual BuilderBase Clear()
        {
            _mapper.Clear();
            return this;
        }
    }
}
