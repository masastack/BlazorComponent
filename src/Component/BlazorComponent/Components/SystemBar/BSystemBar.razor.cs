using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSystemBar : BDomComponentBase, IHasProviderComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected virtual async Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync();
            }
        }

        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply(typeof(CascadingValue<>), typeof(CascadingValue<BSystemBar>), props =>
                {
                    props[nameof(CascadingValue<BSystemBar>.Value)] = this;
                });
        }
    }
}
