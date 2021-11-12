using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ISwitch : IInput<bool>, ISelectable, IRippleable
    {
        public bool IsLoading { get; }
    }
}