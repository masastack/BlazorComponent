using BlazorComponent.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BIcon : BDomComponentBase, IIcon
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        #region IIcon

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public StringNumber Size { get; set; }

        public string Icon { get; set; }

        [Parameter]
        public string Tag { get; set; } = "i";

        public string NewChildren { get; set; }

        public Dictionary<string, object> SvgAttrs { get; set; }

        #endregion

        #region  IThemeable

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        #endregion

        [Parameter]
        public string Color { get; set; }

        [Obsolete("Use OnClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        public virtual async Task HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }

        protected override void OnParametersSet()
        {
            if (Click.HasDelegate)
            {
                OnClick = Click;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var builder = new RenderTreeBuilder();
            ChildContent(builder);

            var frames = builder.GetFrames().Array;
            //todo Array will change
            var frame = builder.GetFrames().Array.FirstOrDefault(u => u.FrameType == RenderTreeFrameType.Text || u.FrameType == RenderTreeFrameType.Markup);

            char[] charsToTrim = { '\r', ' ', '\n' };
            Icon = frame.TextContent.Trim(charsToTrim);
            //is material icons
            if (Icon.IndexOf("-") <= -1 && !RegexHelper.RegexSvgPath(Icon))
            {
                NewChildren = Icon;
            }
        }

    }
}
