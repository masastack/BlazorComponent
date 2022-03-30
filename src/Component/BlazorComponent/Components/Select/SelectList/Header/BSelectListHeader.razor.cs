using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectListHeader<TItem, TItemValue, TValue>
    {
        [Parameter]
        public string Header { get; set; }
    }
}
