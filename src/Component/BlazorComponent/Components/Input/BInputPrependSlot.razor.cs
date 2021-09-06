using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputPrependSlot<TValue, TInput> : ComponentAbstractBase<TInput>
        where TInput : IInput<TValue>
    {
        public RenderFragment PrependContent => Component.PrependContent;

        public string PrependIcon => Component.PrependIcon;
    }
}
