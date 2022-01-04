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

        public IList<TItemValue> InternalValues => Component.InternalValues;

        public TValue InternalValue => Component.InternalValue;
    }
}
