using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputAppendSlot<TInput> where TInput : BInput
    {
        [Parameter]
        public TInput Input { get; set; }

        public string AppendIcon => Input.AppendIcon;

        public RenderFragment AppendContent => Input.AppendContent;

        public ComponentCssProvider CssProvider => Input.CssProvider;

        public ComponentAbstractProvider AbstractProvider => Input.AbstractProvider;
    }
}
