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
        public bool Dark { get; set; }

        [Parameter]
        public bool Column { get; set; }

        public void AddItem(IItem item)
        {
            Items.Add(item);
            SetActiveItem();
        }

        public void NotifyItemChanged(IItem changedItem)
        {
            if (changedItem.IsActive)
            {
                if (Multiple)
                {
                    AddToValues(changedItem.Value);
                }
                else
                {
                    SetValue(changedItem.Value);
                    foreach (var item in Items)
                    {
                        if (item != changedItem)
                        {
                            item.DeActive();
                        }
                    }
                }
            }
            else
            {
                if (Multiple)
                {
                    RemoveFromValues(changedItem.Value);
                }
                else
                {
                    if (Mandatory && !Items.Any(r => r.IsActive))
                    {
                        SetValue(changedItem.Value);
                        changedItem.Active();
                    }
                    else
                    {
                        SetValue(null);
                    }
                }
            }
        }

        private void RemoveFromValues(string value)
        {
            if (Values.Contains(value))
            {
                Values.Remove(value);
                if (ValuesChanged.HasDelegate)
                {
                    ValuesChanged.InvokeAsync(Values);
                }
            }
        }

        private void SetValue(string value)
        {
            if (Value != value)
            {
                Value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(Value);
                }
            }
        }

        private void AddToValues(string value)
        {
            if (!Values.Contains(value))
            {
                Values.Add(value);
                if (ValuesChanged.HasDelegate)
                {
                    ValuesChanged.InvokeAsync(Values);
                }
            }
        }

        protected override void OnParametersSet()
        {
            SetActiveItem();
        }

        private void SetActiveItem()
        {
            foreach (var item in Items)
            {
                if (item.Value != null)
                {
                    if (Multiple)
                    {
                        if (Values.Contains(item.Value))
                        {
                            item.Active();
                        }
                    }
                    else
                    {
                        if (Value == item.Value)
                        {
                            item.Active();
                        }
                    }
                }
            }
        }
    }
}
