using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectHiddenInput<TItem, TValue, TInput> where TInput : ISelect<TItem, TValue>
    {
        public bool Multiple => Component.Multiple;

        public List<TValue> Values => Component.Values;

        public TValue Value => Component.Value;
    }
}
