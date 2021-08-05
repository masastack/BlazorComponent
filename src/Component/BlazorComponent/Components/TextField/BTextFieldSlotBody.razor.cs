using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldSlotBody<TValue,TInput> where TInput:ITextField<TValue>
    {
        public string PrependInnerIcon => Input.PrependInnerIcon;

        public RenderFragment PrependInnerContent => Input.PrependInnerContent;

        public string AppendIcon => Input.AppendIcon;

        public RenderFragment AppendContent => Input.AppendContent;

        public ComponentCssProvider CssProvider => Input.CssProvider;

        public bool Outlined => Input.Outlined;

        public string InnerHtml => Input.LegendInnerHTML;

        public bool ShowLabel => Input.ShowLabel;

        public string Prefix => Input.Prefix;

        public string Suffix => Input.Suffix;

        public TValue Value
        {
            get
            {
                return Input.Value;
            }
            set
            {
                Input.Value = value;
            }
        }

        public EventCallback<TValue> ValueChanged => Input.ValueChanged;

        public bool Autofocus => Input.Autofocus;

        public bool IsDisabled => Input.IsDisabled;

        public string Placeholder => (Input.PersistentPlaceholder || Input.IsFocused || !HasLabel) ? Input.Placeholder : null;

        public bool Readonly => Input.IsReadonly;

        public string Type => Input.Type;

        public bool IsFocused
        {
            get
            {
                return Input.IsFocused;
            }
            set
            {
                Input.IsFocused = value;
            }
        }

        public EventCallback<FocusEventArgs> OnBlur => Input.OnBlur;

        public EventCallback<FocusEventArgs> OnFocus => Input.OnFocus;

        public EventCallback<KeyboardEventArgs> OnKeyDown => Input.OnKeyDown;

        public bool Clearable => Input.Clearable;

        public bool IsDirty => Input.IsDirty;

        public virtual string ClearIcon => Input.ClearIcon;

        public StringBoolean Loading => Input.Loading;

        public RenderFragment ProgressContent => Input.ProgressContent;

        public string InputTag => Input.Tag;

        public Dictionary<string, object> InputAttrs => Input.Attrs;

        public virtual async Task HandleOnChange(ChangeEventArgs args)
        {
            try
            {
                Value = (TValue)Convert.ChangeType(args.Value, typeof(TValue));
            }
            catch (Exception)
            {
                Value = default;
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
        }

        public virtual async Task HandleOnBlur(FocusEventArgs args)
        {
            IsFocused = false;

            if (OnBlur.HasDelegate)
            {
                await OnBlur.InvokeAsync(args);
            }
        }

        public virtual Task HandleOnInput(ChangeEventArgs args)
        {
            //TODO:badInput
            return Task.CompletedTask;
        }

        public virtual async Task HandleOnFocus(FocusEventArgs args)
        {
            //TODO:focus element
            if (!IsFocused)
            {
                IsFocused = true;
                if (OnFocus.HasDelegate)
                {
                    await OnFocus.InvokeAsync(args);
                }
            }
        }

        public virtual async Task HandleOnKeyDown(KeyboardEventArgs args)
        {
            //TODO:onchange
            if (OnKeyDown.HasDelegate)
            {
                await OnKeyDown.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnClear(MouseEventArgs args)
        {
            //TODO:autofocus
            Value = default;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
        }
    }
}
