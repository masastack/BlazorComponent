using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputLabel<TValue, TInput> : ComponentPartBase<TInput>
        where TInput : IInput<TValue>
    {
        public bool HasLabel => Component.HasLabel;

        public string Label => Component.Label;

        public RenderFragment LabelContent => Component.LabelContent;
    }
}
