using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelect<TItem, TValue> : BDomComponentBase
    {
        private bool _shouldReformatText = true;
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

        public ElementReference PopoverRef { get; set; }

        protected List<string> _text = new();

        protected virtual List<string> FormatText(TValue value)
        {
            return Items
                .Where(u => ItemValue(u).Equals(value))
                .Select(ItemText).ToList();
        }

        protected virtual List<string> FormatText(IEnumerable<TValue> values)
        {
            return Items
                .Where(u => values.Contains(ItemValue(u)))
                .Select(ItemText).ToList();
        }

        #region Binding

        private TValue _value;

        [Parameter]
        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;

                if (_shouldReformatText && ItemValue != null)
                    _text = FormatText(value);

                _shouldReformatText = true;
            }
        }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        private List<TValue> _values = new();

        [Parameter]
        public List<TValue> Values
        {
            get => _values;
            set
            {
                _values = value ?? new List<TValue>();

                if (_shouldReformatText && ItemValue != null)
                    _text = FormatText(value);

                _shouldReformatText = true;
            }
        }

        [Parameter]
        public EventCallback<List<TValue>> ValuesChanged { get; set; }

        #endregion

        [Parameter]
        public Func<TItem, string> ItemText { get; set; } = null!;

        [Parameter]
        public Func<TItem, TValue> ItemValue { get; set; } = null!;

        [Parameter]
        public IReadOnlyList<TItem> Items { get; set; } = new List<TItem>();

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected bool Slot { get; set; }

        protected TItem SelectNode { get; set; }

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

        public async Task SetSelectedAsync(string text, TValue value)
        {
            //SelectNode = item;

            _shouldReformatText = false;

            if (Multiple)
            {
                if (!_values.Contains(value))
                {
                    _values.Add(value);
                    _text.Add(text);
                }
            }
            else
            {
                _value = value;
                _text.Clear();
                _text.Add(text);
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

        public async Task RemoveSelectedAsync(string text, TValue value)
        {
            _shouldReformatText = false;

            _values.Remove(value);
            _text.Remove(text);

            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(_values);
            }
        }
    }
}