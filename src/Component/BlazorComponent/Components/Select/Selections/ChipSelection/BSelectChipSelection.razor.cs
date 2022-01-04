using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectChipSelection<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public bool Last { get; set; }

        protected string GetText(TItem item) => Component.GetText(item);
    }
}
