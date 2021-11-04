using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTimePickerClock
    {
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
