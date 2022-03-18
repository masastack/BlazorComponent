using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTooltip : BMenuable
    {
        private bool _isActive;

        public BTooltip()
        {
            OpenOnHover = true;
            OpenOnFocus = true;
        }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Tag { get; set; } = "span";

        [Parameter]
        public string Transition { get; set; }

        protected double CalculatedLeft
        {
            get
            {
                var activator = Dimensions.Activator;
                var content = Dimensions.Content;
                if (activator == null || content == null) return 0;

                var unknown = !Bottom && !Left && !Top && !Right;
                var activatorLeft = Attach != null ? activator.OffsetLeft : activator.Left;
                double left = 0;

                if (Top || Bottom || unknown)
                {
                    left = activatorLeft + (activator.Width / 2) - (content.Width / 2);
                }
                else if (Left || Right)
                {
                    left = activatorLeft + (Right ? activator.Width : -content.Width) + (Right ? 10 : -10);
                }

                if (NudgeLeft != null)
                {
                    var (_, nudgeLeft) = NudgeLeft.TryGetNumber();
                    left -= nudgeLeft;
                }

                if (NudgeRight != null)
                {
                    var (_, nudgeRight) = NudgeRight.TryGetNumber();
                    left += nudgeRight;
                }

                return CalcXOverflow(left, content.Width);
            }
        }

        protected double CalculatedTop
        {
            get
            {
                var activator = Dimensions.Activator;
                var content = Dimensions.Content;
                if (activator == null || content == null) return 0;

                var activatorTop = Attach != null ? activator.OffsetTop : activator.Top;
                double top = 0;

                if (Top || Bottom)
                {
                    top = activatorTop + (Bottom ? activator.Height : -content.Height) + (Bottom ? 10 : -10);
                }
                else if (Left || Right)
                {
                    top = activatorTop + (activator.Height / 2) - (content.Height / 2);
                }

                if (NudgeTop != null)
                {
                    var (_, nudgeTop) = NudgeTop.TryGetNumber();
                    top -= nudgeTop;
                }

                if (NudgeBottom != null)
                {
                    var (_, nudgeBottom) = NudgeBottom.TryGetNumber();
                    top += nudgeBottom;
                }

                if (Attach == null)
                {
                    top += PageYOffset;
                }

                return CalcYOverflow(top);
            }
        }

        protected override Task HandleOnMouseEnterAsync(MouseEventArgs args)
        {
            _isActive = true;
            return base.HandleOnMouseEnterAsync(args);
        }

        protected override Task HandleOnMouseLeaveAsync(MouseEventArgs args)
        {
            _isActive = false;
            return base.HandleOnMouseLeaveAsync(args);
        }

        protected override Task OnIsActiveSetAsync(bool isActive)
        {
            //When enter and leave been triggered too frequently
            //Leave may before enter invoke this method
            //We record isActive
            //So isActive will always be same to leave or enter state
            return base.OnIsActiveSetAsync(_isActive);
        }
    }
}