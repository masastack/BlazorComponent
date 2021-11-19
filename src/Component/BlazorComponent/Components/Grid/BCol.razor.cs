using System;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BCol : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public StringNumber Cols { get; set; }

        [Parameter]
        public StringNumber Order { get; set; }

        [Parameter]
        public StringNumber Offset { get; set; }

        [Parameter]
        public virtual string Tag { get; set; } = "div";

        private string GutterStyle { get; set; }

        internal void RowGutterChanged((int horizontalGutter, int verticalGutter) gutter)
        {
            GutterStyle = "";
            if (gutter.horizontalGutter > 0)
            {
                GutterStyle = $"padding-left: {gutter.horizontalGutter / 2}px; padding-right: {gutter.horizontalGutter / 2}px;";
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
