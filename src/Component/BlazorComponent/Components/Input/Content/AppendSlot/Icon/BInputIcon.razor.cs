using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BInputIcon<TValue, TInput> : ComponentPartBase<TInput>
        where TInput : IInput<TValue>
    {
        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }
    }
}
