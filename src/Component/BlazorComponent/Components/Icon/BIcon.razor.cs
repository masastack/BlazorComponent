using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;
using System.Threading.Tasks;

namespace BlazorComponent
{
    using StringNumber = OneOf<string, int>;

    public abstract partial class BIcon : BDomComponentBase
    {
        private string _icon;

        // TODO: 维护内置颜色列表
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber? Size { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private async Task HandleOnClick(MouseEventArgs args)
        {
            if (Click.HasDelegate)
            {
                await Click.InvokeAsync(args);
            }
        }

        protected override void OnInitialized()
        {
            _icon = GetChildContentText(ChildContent);

            base.OnInitialized();
        }
    }
}
