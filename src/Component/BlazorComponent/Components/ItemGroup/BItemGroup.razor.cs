using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BItemGroup : BDomComponentBase
    {
        internal readonly GroupType _groupType;

        public BItemGroup(GroupType groupType)
        {
            _groupType = groupType;
        }

        protected List<StringNumber> _values = new();

        public List<StringNumber> AllKeys { get; set; } = new();

        public List<BItemBase<BItemGroup>> Items { get; set; } = new();

        [Parameter]
        public StringNumber Max { get; set; }

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

        [Parameter]
        public bool Mandatory { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
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

                if (Max == null || _values.Count < Max.TryGetNumber().number)
                {
                    _values.Add(key);
                }
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
    }
}