using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectListWithSlot<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        protected RenderFragment NoDataContent => Component.NoDataContent;

        protected RenderFragment PrependItemContent => Component.PrependItemContent;

        protected RenderFragment AppendItemContent => Component.AppendItemContent;
    }
}
