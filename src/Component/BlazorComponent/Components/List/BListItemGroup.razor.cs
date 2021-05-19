using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BListItemGroup : BDomComponentBase
    {
        [Parameter] public string Value { get; set; }

        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public List<string> Values { get; set; }
        [Parameter]
        public EventCallback<List<string>> ValuesChanged { get; set; }

        public async Task ToggleSelectAsync(string key)
        {
            if (Multiple)
            {
                if (Values.Contains(key))
                {
                    Values.Remove(key);
                }
                else
                {
                    Values.Add(key);
                }
            }
            else
            {
                Value = key;
            }

            await InvokeStateHasChangedAsync();
        }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}