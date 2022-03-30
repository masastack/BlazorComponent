using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableHeader : BDomComponentBase
    {
        [Parameter]
        public List<DataTableHeader> Headers { get; set; } = new();
    }
}
