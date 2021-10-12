using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanels
    {
        public BExpansionPanels(GroupType groupType) : base(groupType)
        {
        }

        public List<StringNumber> NextActiveKeys { get; set; } = new();

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Accordion { get; set; }

        [Parameter]
        public bool Focusable { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        public async override Task Toggle(StringNumber value)
        {
            if (_values.Contains(value))
            {
                _values.Remove(value);

                var index = AllValues.IndexOf(value);
                if (index > 1)
                {
                    NextActiveKeys.Remove(AllValues[index - 1]);
                }
            }
            else
            {
                if (!Multiple)
                {
                    _values.Clear();
                    NextActiveKeys.Clear();
                }

                _values.Add(value);

                var index = AllValues.IndexOf(value);
                if (index > 1)
                {
                    NextActiveKeys.Add(AllValues[index - 1]);
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