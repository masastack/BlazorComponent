using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent
{
    public partial class BTableCol : BDomComponentBase
    {
        private RenderFragment _childContent;
        private bool _childContentChanged;

        protected string Title { get; set; }

        [CascadingParameter]
        public ITable Table { get; set; }

        [Parameter]
        public bool Ellipsis { get; set; }

        private bool? _showTitle;

        [Parameter]
        public bool ShowTitle
        {
            get => _showTitle ?? Ellipsis;
            set => _showTitle = value;
        }

        [Parameter]
        public RenderFragment ChildContent
        {
            get => _childContent;
            set
            {
                _childContent = value;
                _childContentChanged = true;
            }
        }

        protected override void OnParametersSet()
        {
            if (Ellipsis && _childContentChanged)
            {
                Title = FormatChildContent(ChildContent);
            }
        }

        protected override void OnInitialized()
        {
            if (Ellipsis)
            {
                Table.SetTableLayoutFixed();
            }
        }

        private string FormatChildContent(RenderFragment content)
        {
            var builder = new RenderTreeBuilder();
            content(builder);

            if (builder.GetFrames().Count != 1) return null;

            return builder.GetFrames().Array[0].TextContent;
        }
    }
}