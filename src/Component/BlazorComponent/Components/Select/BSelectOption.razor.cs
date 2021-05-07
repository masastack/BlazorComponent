using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectOption<T> : BDomComponentBase
    {
        [Parameter]
        public bool Checked { get; set; }

        [CascadingParameter]
        protected BSelect<T> SelectWrapper { get; set; }

        [Parameter]
        public T Value { get; set; }

        [Parameter]
        public string Key { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
