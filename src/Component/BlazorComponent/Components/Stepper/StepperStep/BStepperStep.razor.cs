using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BStepperStep : BDomComponentBase
    {
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        public virtual Task HandleOnClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
