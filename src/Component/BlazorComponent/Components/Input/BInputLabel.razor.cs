using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputLabel<TInput> : ComponentAbstractBase<TInput>
          where TInput : IInput
    {
        public bool HasLabel => Component.HasLabel;

        public string Label => Component.Label;

        public RenderFragment LabelContent => Component.LabelContent;
    }
}
