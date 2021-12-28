using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectHiddenInput<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        public bool Multiple => Component.Multiple;

        public IList<TItemValue> Values => Component.Values;

        public TValue InternalValue => Component.InternalValue;
    }
}
