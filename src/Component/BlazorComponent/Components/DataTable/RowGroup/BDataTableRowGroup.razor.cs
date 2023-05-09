using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableRowGroup
    {
        [Parameter]
        public RenderFragment? RowHeaderContent { get; set; }

        [Parameter]
        public RenderFragment? RowContentContent { get; set; }

        [Parameter]
        public RenderFragment? ColumnHeaderContent { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected virtual string? HeaderClass { get; }
    }
}
