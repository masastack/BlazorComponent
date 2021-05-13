using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelect<TItem> : BDomComponentBase
    {
        protected bool _visible;
        protected bool _focused;
        protected string _icon;

        // TODO:
        protected virtual string LegendStyle { get; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Filled { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Solo { get; set; }

        [Parameter]
        public bool Multiple { get; set; }

        [Parameter]
        public bool Chips { get; set; }

        [Parameter]
        public string Hint { get; set; }

        [Parameter]
        public bool PersistentHint { get; set; }

        protected List<string> _text = new();

        #region Binding

        private string _value;

        [Parameter]
        public string Value
        {
            get => _value;
            set
            {
                _text = Items.Where(u => ItemValue(u) == value)
                    .Select(ItemText).ToList();

                _value = value;
            }
        }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        private List<string> _values = new();

        [Parameter]
        public IEnumerable<string> Values
        {
            get => _values;
            set
            {
                _text = Items
                    .Where(u => value.Contains(ItemValue(u)))
                    .Select(ItemText).ToList();

                _values = value.ToList();
            }
        }

        [Parameter]
        public EventCallback<IEnumerable<string>> ValuesChanged { get; set; }

        #endregion

        [Parameter]
        public Func<TItem, string> ItemText { get; set; } = new Func<TItem, string>(u => u.ToString());

        [Parameter]
        public Func<TItem, string> ItemValue { get; set; } = new Func<TItem, string>(u => u.ToString());

        [Parameter]
        public IReadOnlyList<TItem> Items { get; set; }

        protected RenderFragment SelectArrowContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected bool Slot { get; set; }

        protected TItem SelectNode { get; set; }

        protected override Task OnInitializedAsync()
        {
            Items.ForEach(u =>
            {
                var v = ItemValue.Invoke(u);

                if (Value == v || Values.Contains(v))
                {
                    var t = ItemText.Invoke(u);

                    if (!Multiple)
                        _text.Clear();

                    _text.Add(t);
                }
            });

            return base.OnInitializedAsync();
        }

        protected virtual void HandleOnBlur(FocusEventArgs args)
        {
            _focused = false;
        }

        protected virtual void Click(MouseEventArgs args)
        {
            _focused = true;
            _visible = true;
        }

        public void SetVisible(bool visible)
        {
            _visible = visible;
            InvokeStateHasChanged();
        }

        public async Task SetSelectedAsync(TItem item)
        {
            SelectNode = item;

            var v = ItemValue.Invoke(item);

            if (Multiple)
            {
                if (!_values.Contains(v))
                {
                    _values.Add(v);
                }
            }
            else
            {
                _value = v;
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(_value);
            }

            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(_values);
            }
        }

        public async Task RemoveSelectedAsync(TItem item)
        {
            var v = ItemValue.Invoke(item);
            _values.Remove(v);

            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(_values);
            }
        }

        private bool IsSelected(TItem item)
        {
            var v = ItemValue.Invoke(item);

            return Multiple ? Values.Contains(v) : Value == v;
        }
    }
}