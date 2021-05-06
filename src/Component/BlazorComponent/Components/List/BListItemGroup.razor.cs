using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BListItemGroup : BDomComponentBase
    {
        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        public async Task ChangeValue(string key)
        {
            Value = key;
            await InvokeStateHasChangedAsync();
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
