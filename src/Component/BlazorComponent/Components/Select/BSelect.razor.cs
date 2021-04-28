using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSelect<TItem> : BDomComponentBase
    {
        protected CssBuilder ControlCssBuilder = new ();
        protected CssBuilder SlotCssBuilder = new ();
        protected CssBuilder SelectSlotCssBuilder = new ();
        protected CssBuilder LabelCssBuilder = new ();
        protected StyleBuilder LabelStyleCssBuilder = new ();
        protected CssBuilder SelectorCssBuilder = new ();
        protected CssBuilder SelectedCssBuilder = new ();
        protected CssBuilder SelectInputCssBuilder = new ();
        protected CssBuilder SelectArrowCssBuilder= new ();
        protected CssBuilder SelectArrowIconCssBuilder = new ();
        protected CssBuilder HitCssBuilder = new ();

        protected CssBuilder ListCssBuilder = new ();

        protected bool _visible;
        protected bool _focused;
        protected string _icon;

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

        public string HitMessage { get; set; }

        public string Text { get; set; }
        public string Value { get; set; }

        [Parameter]
        public Func<TItem, string> ItemText { get; set; }

        [Parameter]
        public Func<TItem, string> ItemValue { get; set; }

        [Parameter]
        public IReadOnlyList<TItem> Items { get; set; }

        protected RenderFragment SelectArrowContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

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

        public void SetSelected(TItem value)
        {
            Text = ItemText != null
                ? ItemText.Invoke(value)
                : value.ToString();

            Value = ItemValue != null
                ? ItemValue.Invoke(value)
                : value.ToString();
        }
    }
}
