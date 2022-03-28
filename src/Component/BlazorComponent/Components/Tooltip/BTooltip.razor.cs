using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTooltip : BMenuable
    {
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
    }
}