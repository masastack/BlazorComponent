using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectSelections<TItem, TValue, TInput> where TInput : ISelect<TItem, TValue>
    {
        public List<string> Text => Component.Text;

        public bool Chips => Component.Chips;

        public bool Multiple => Component.Multiple;
    }
}
