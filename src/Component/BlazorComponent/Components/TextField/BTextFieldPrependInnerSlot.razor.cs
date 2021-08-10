using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldPrependInnerSlot<TValue, TInput> where TInput : ITextField<TValue>
    {
        public RenderFragment PrependInnerContent => Component.PrependInnerContent;

        public string PrependInnerIcon => Component.PrependInnerIcon;
    }
}
