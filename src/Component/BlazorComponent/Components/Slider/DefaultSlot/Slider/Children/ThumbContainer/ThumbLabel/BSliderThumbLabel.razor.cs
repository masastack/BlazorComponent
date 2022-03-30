using Microsoft.AspNetCore.Components;

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
