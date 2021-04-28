using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectOption<T> : BDomComponentBase
    {
        [CascadingParameter]
        protected BSelect<T> SelectWrapper { get; set; }

        [Parameter]
        public T Value { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
