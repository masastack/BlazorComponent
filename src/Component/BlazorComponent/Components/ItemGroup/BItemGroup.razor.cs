using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BItemGroup : ItemGroupBase
    {
        public BItemGroup() : base(GroupType.ItemGroup)
        {
        }

        public BItemGroup(GroupType groupType) : base(groupType)
        {
        }

        [Parameter]
        public StringNumber Max { get; set; }

        public async virtual Task Toggle(StringNumber value)
        {
            if (_values.Contains(value))
            {
                _values.Remove(value);
            }
            else
            {
                if (!Multiple)
                {
                    _values.Clear();
                }

                if (Max == null || _values.Count < Max.TryGetNumber().number)
                {
                    _values.Add(value);
                }
            }

            if (Mandatory && _values.Count == 0)
            {
                _values.Add(value);
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