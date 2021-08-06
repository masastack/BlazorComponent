using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputPrependSlot<TInput> where TInput : IInput
    {
        public RenderFragment PrependContent => Component.PrependContent;

        public string PrependIcon => Component.PrependIcon;
    }
}
