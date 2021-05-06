using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BListItemGroup : BDomComponentBase
    {
        [Parameter] public string Value { get; set; }

        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        private List<string> _values = new List<string>();

        [Parameter]
        public IEnumerable<string> Values
        {
            get => _values;
            set => _values = value.ToList();
        }

        [Parameter] public EventCallback<IEnumerable<string>> ValuesChanged { get; set; }

        public async Task ToggleSelectAsync(string key)
        {
            if (Multiple)
            {
                if (_values.Contains(key))
                {
                    _values.Remove(key);
                }
                else
                {
                    _values.Add(key);
                }
            }
            else
            {
                Value = key;
            }

            await InvokeStateHasChangedAsync();
        }

        [Parameter] public bool Multiple { get; set; }

        [Parameter] public string Color { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}