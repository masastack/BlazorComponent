using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BItemGroup : BDomComponentBase
    {
        protected List<StringNumber> _values = new();

        public List<StringNumber> AllKeys { get; set; } = new();

        [Parameter]
        public StringNumber Value
        {
            get => _values.LastOrDefault();
            set
            {
                _values.Clear();
                _values.Add(value);
            }
        }

        [Parameter]
        public EventCallback<StringNumber> ValueChanged { get; set; }

        [Parameter]
        public List<StringNumber> Values
        {
            get => _values;
            set => _values = value;
        }

        [Parameter]
        public EventCallback<List<StringNumber>> ValuesChanged { get; set; }

        public async virtual Task Toggle(StringNumber key)
        {
            if (_values.Contains(key))
            {
                _values.Remove(key);
            }
            else
            {
                if (!Multiple)
                {
                    _values.Clear();
                }

                _values.Add(key);
            }

            if (Mandatory && _values.Count == 0)
            {
                _values.Add(key);
            }

            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(_values);
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(_values.LastOrDefault());
            }

            StateHasChanged();
        }

        public GroupType Type { get; set; }

        public void InitType(GroupType type)
        {
            Type = type;
            StateHasChanged();
        }

        [Parameter]
        public bool Mandatory { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
