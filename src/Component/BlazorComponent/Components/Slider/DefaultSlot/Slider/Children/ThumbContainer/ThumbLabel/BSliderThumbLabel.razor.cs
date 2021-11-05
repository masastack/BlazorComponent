using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSliderThumbLabel<TValue, TInput> where TInput : ISlider<TValue>
    {
        [Parameter]
        public int Index { get; set; }

        public RenderFragment<int> ThumbLabelContent => Component.ThumbLabelContent;

        public bool ShowThumbLabelContainer => Component.ShowThumbLabelContainer;
    }
}
