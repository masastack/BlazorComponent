using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BItemGroup : BDomComponentBase
    {
        public string Type { get; set; }

        public void InitType(string type)
        {
            Type = type;
            StateHasChanged();
        }

        protected List<IItem> Items { get; set; } = new();

        [Parameter]
        public bool Mandatory { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public List<string> Values { get; set; } = new();

        [Parameter]
        public EventCallback<List<string>> ValuesChanged { get; set; }

        [Parameter]
        public bool Column { get; set; }

        public void AddItem(IItem item)
        {
            Items.Add(item);
        }

        public void NotifyItemChanged(IItem changedItem)
        {
            Value = changedItem.Value;
            if (ValueChanged.HasDelegate)
            {
                ValueChanged.InvokeAsync(Value);
            }
            StateHasChanged();
        }
    }
}
