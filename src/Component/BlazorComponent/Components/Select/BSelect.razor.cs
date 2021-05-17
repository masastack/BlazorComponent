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

        protected List<string> _text = new();

        protected virtual List<string> FormatText(string value)
        {
            return Items
                .Where(u => ItemValue(u) == value)
                .Select(ItemText).ToList();
        }

        protected virtual List<string> FormatText(IEnumerable<string> values)
        {
            return Items
                .Where(u => values.Contains(ItemValue(u)))
                .Select(ItemText).ToList();
        }

        #region Binding

        private string _value;

        [Parameter]
        public string Value
        {
            get => _value;
            set
            {
                _value = value;

                if (_shouldReformatText)
                    _text = FormatText(value);

                _shouldReformatText = true;
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
                _values = value.ToList();

                if (_shouldReformatText)
                    _text = FormatText(value);

                _shouldReformatText = true;
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
        public IReadOnlyList<TItem> Items { get; set; } = new List<TItem>();

        protected RenderFragment SelectArrowContent { get; set; }

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

        public async Task SetSelectedAsync(TItem item)
        {
            //SelectNode = item;

            _shouldReformatText = false;

            var v = ItemValue.Invoke(item);

            if (Multiple)
            {
                if (!_values.Contains(v))
                {
                    _values.Add(v);
                    _text.Add(ItemText.Invoke(item));
                }
            }
            else
            {
                _value = v;
                _text.Clear();
                _text.Add(ItemText.Invoke(item));
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
            _shouldReformatText = false;

            _values.Remove(ItemValue.Invoke(item));
            _text.Remove(ItemText.Invoke(item));

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