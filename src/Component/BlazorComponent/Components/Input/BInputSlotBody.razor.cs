using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputSlotBody<TInput> where TInput : BInput
    {
        [Parameter]
        public TInput Input { get; set; }

        public bool HasLabel => Input.HasLabel;

        public ComponentAbstractProvider AbstractProvider => Input.AbstractProvider;

        public RenderFragment InputChildContent => Input.ChildContent;

        public string Label => Input.Label;

        public RenderFragment LabelContent => Input.LabelContent;
    }
}
