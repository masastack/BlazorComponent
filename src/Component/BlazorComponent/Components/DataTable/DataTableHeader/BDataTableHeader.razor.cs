using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableHeader : BDomComponentBase
    {
        [Parameter]
        public List<DataTableHeader> Headers { get; set; } = new();

        [Parameter]
        public string GroupText { get; set; } = null!;

        [Parameter]
        public bool IsMobile { get; set; }
    }
}
