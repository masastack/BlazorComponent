using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanels : BItemGroup
    {
        public BExpansionPanels() : base(GroupType.ExpansionPanels)
        {
        }

        public List<StringNumber> NextActiveKeys { get; } = new();

        [Parameter]
        public bool Accordion { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Focusable { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Hover { get; set; }

        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public bool Popout { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        public async override Task ToggleAsync(StringNumber value)
        {
            if (_values.Contains(value))
            {
                Remove(value);
            }
            else
            {
                if (!Multiple)
                {
                    _values.Clear();
                    NextActiveKeys.Clear();
                }

                if (Max == null || _values.Count < Max.TryGetNumber().number)
                {
                    Add(value);
                }
            }

            if (Mandatory && _values.Count == 0)
            {
                Add(value);
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(_values.LastOrDefault());
            }
            else if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(_values);
            }
            else
            {
                StateHasChanged();
            }
        }


        private void Add(StringNumber value)
        {
            _values.Add(value);

            var index = AllValues.IndexOf(value);
            if (index > 1)
            {
                NextActiveKeys.Add(AllValues[index - 1]);
            }
        }

        private void Remove(StringNumber value)
        {
            _values.Remove(value);

            var index = AllValues.IndexOf(value);
            if (index > 1)
            {
                NextActiveKeys.Remove(AllValues[index - 1]);
            }
        }
    }
}