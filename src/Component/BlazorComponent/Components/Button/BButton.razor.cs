using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;

namespace BlazorComponent
{
    using StringNumber = OneOf<string, int>;

    public abstract partial class BButton : BDomComponentBase
    {
        /// <summary>
        /// The background color
        /// </summary>
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Block { get; set; }

        /// <summary>
        /// Floating
        /// </summary>
        [Parameter]
        public bool Fab { get; set; }

        [Parameter]
        public bool Icon { get; set; }

        [Parameter]
        public bool Plain { get; set; }

        [Parameter]
        public bool Text { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        [Parameter]
        public bool XSmall { get; set; }

        [Parameter]
        public StringNumber? Width { get; set; }

        [Parameter]
        public StringNumber? MaxWidth { get; set; }

        [Parameter]
        public StringNumber? MinWidth { get; set; }

        [Parameter]
        public StringNumber? Height { get; set; }

        [Parameter]
        public StringNumber? MaxHeight { get; set; }

        [Parameter]
        public StringNumber? MinHeight { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        [Parameter]
        public bool ClickStopPropagation { get; set; }

        [Parameter]
        public RenderFragment LoadingFragment { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private async Task HandleOnClick(MouseEventArgs args)
        {
            if (Click.HasDelegate)
            {
                await Click.InvokeAsync(args);
            }
        }
    }
}
