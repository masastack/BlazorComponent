using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract partial class BButton : BGroupItem<BItemGroup>, IThemeable
    {
        public BButton() : base(GroupType.ButtonGroup)
        {
        }

        /// <summary>
        /// The background color
        /// </summary>
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Block { get; set; }

        [Parameter]
        public string Type { get; set; } = "button";

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
        public bool Absolute { get; set; }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Obsolete("Use OnClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public bool StopPropagation { get; set; }

        [Obsolete("Use LoaderContent instead.")]
        [Parameter]
        public RenderFragment Loader { get; set; }

        [Parameter]
        public RenderFragment LoaderContent { get; set; }

        public virtual bool IsDark { get; }
        public bool IsActive { get; set; }
        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public bool Dark { get; set; }


        protected override void OnParametersSet()
        {
            if (Click.HasDelegate)
            {
                OnClick = Click;
            }

            if (Loader != null)
            {
        }

        }

            await ToggleItem();
                ItemGroup.NotifyItemChanged(this);
            }

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }
    }
}
