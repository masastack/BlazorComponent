using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectListAction<TItem, TItemValue, TValue>
    {
        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public bool Value { get; set; }
    }
}
