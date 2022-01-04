using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IAutocomplete<TItem, TItemValue, TValue> : ISelect<TItem, TItemValue, TValue>
    {
        bool HasSlot { get; }
    }
}
