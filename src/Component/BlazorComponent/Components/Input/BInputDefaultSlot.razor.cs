using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputDefaultSlot<TInput> where TInput : IInput
    {
        public bool HasLabel => Component.HasLabel;

        public RenderFragment ComponentChildContent => Component.ChildContent;

        public string Label => Component.Label;

        public RenderFragment LabelContent => Component.LabelContent;
    }
}
