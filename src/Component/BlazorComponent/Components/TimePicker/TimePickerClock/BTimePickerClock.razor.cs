using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BTimePickerClock
    {
        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        public ElementReference InnerClockElement { get; set; }

        protected virtual Task HandleOnMouseDownAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleOnMouseUpAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleOnMouseLeaveAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleOnDragMoveAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual Task HandleOnWheelAsync(WheelEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
