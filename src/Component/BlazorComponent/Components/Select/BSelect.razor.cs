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

        [Parameter] public string Label { get; set; }

        [Parameter] public bool Dense { get; set; }

        [Parameter] public bool Disabled { get; set; }

        [Parameter] public bool Filled { get; set; }

        [Parameter] public bool Outlined { get; set; }

        [Parameter] public bool Solo { get; set; }

        [Parameter] public bool Multiple { get; set; }

        [Parameter] public bool Chips { get; set; }

        [Parameter] public string Hint { get; set; }

        [Parameter] public bool PersistentHint { get; set; }

        protected List<string> _text = new();

        #region Binding

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        private List<string> _values = new();

        [Parameter]
        public IEnumerable<string> Values
        {
            get => _values;
            set => _values = value.ToList();
        }

        [Parameter] public EventCallback<IEnumerable<string>> ValuesChanged { get; set; }

        #endregion

        [Parameter] public Func<TItem, string> ItemText { get; set; }

        [Parameter] public Func<TItem, string> ItemValue { get; set; }

        [Parameter] public IReadOnlyList<TItem> Items { get; set; }

        protected RenderFragment SelectArrowContent { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override Task OnInitializedAsync()
        {
            Items.ForEach(u =>
            {
                var v = ItemValue != null
                        ? ItemValue.Invoke(u)
                        : u.ToString();

                if (Value == v || Values.Contains(v))
                {
                    var t = ItemText != null
                            ? ItemText.Invoke(u)
                            : u.ToString();

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

        public async Task SetSelectedAsync(TItem value)
        {
            var t = ItemText != null
                ? ItemText.Invoke(value)
                : value.ToString();

            var v = ItemValue != null
                ? ItemValue.Invoke(value)
                : value.ToString();

            if (Multiple)
            {
                if (!_text.Contains(t))
                {
                    _text.Add(t);
                }
            }
            else
            {
                _text.Clear();
                _text.Add(t);
            }

            if (Multiple)
            {
                if (!_values.Contains(v))
                {
                    _values.Add(v);
                }
            }
            else
            {
                Value = v;
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(Values);
            }
        }

        public async Task RemoveSelectedAsync(TItem value)
        {
            var t = ItemText != null
                ? ItemText.Invoke(value)
                : value.ToString();

            var v = ItemValue != null
                ? ItemValue.Invoke(value)
                : value.ToString();

            _text.Remove(t);
            _values.Remove(v);

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(Values);
            }
        }

        private bool IsSelected(TItem item)
        {
            var v = ItemValue != null
                    ? ItemValue.Invoke(item)
                    : item.ToString();

            return Multiple ? Values.Contains(v) : Value == v;
        }
    }
}