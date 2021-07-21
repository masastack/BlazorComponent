using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanels : BDomComponentBase
    {
        private List<StringNumber> _values = new();

        public List<StringNumber> NextActiveKeys { get; set; } = new();

        /// <summary>
        /// Records the all keys.
        /// </summary>
        public List<StringNumber> AllKeys { get; set; } = new();

        [Parameter]
        public List<StringNumber> Values
        {
            get => _values;
            set => _values = value;
        }

        [Parameter]
        public EventCallback<List<StringNumber>> ValuesChanged { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public bool Accordion { get; set; }

        [Parameter]
        public bool Focusable { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        public async Task TogglePanel(StringNumber key)
        {
            if (_values.Contains(key))
            {
                _values.Remove(key);

                var index = AllKeys.IndexOf(key);
                if (index > 1)
                {
                    NextActiveKeys.Remove(AllKeys[index - 1]);
                }
            }
            else
            {
                if (!Multiple)
                {
                    _values.Clear();
                    NextActiveKeys.Clear();
                }

                _values.Add(key);

                var index = AllKeys.IndexOf(key);
                if (index > 1)
                {
                    NextActiveKeys.Add(AllKeys[index - 1]);
                }
            }

            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(_values);
            }

            StateHasChanged();
        }
    }
}
