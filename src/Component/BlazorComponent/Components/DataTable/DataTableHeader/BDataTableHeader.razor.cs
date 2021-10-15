using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BDataTableHeader : BDomComponentBase
    {
        [Parameter]
        public List<DataTableHeader> Headers { get; set; } = new();
    }
}
