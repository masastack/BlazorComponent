using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldInput<TValue, TInput> where TInput : ITextField<TValue>
    {
        public TValue Value
        {
            get
            {
                return Component.Value;
            }
            set
            {
                Component.Value = value;
            }
        }

        public EventCallback<TValue> ValueChanged => Component.ValueChanged;

        public bool Autofocus => Component.Autofocus;

        public bool IsDisabled => Component.IsDisabled;

        public bool HasLabel => Component.HasLabel;

        public string Placeholder => (Component.PersistentPlaceholder || Component.IsFocused || !HasLabel) ? Component.Placeholder : null;

        public bool Readonly => Component.IsReadonly;

        public bool IsFocused
        {
            get
            {
                return Component.IsFocused;
            }
            set
            {
                Component.IsFocused = value;
            }
        }

        public string InputTag => Component.Tag;

        public Dictionary<string, object> InputAttrs => Component.Attrs;

        public EventCallback<FocusEventArgs> OnBlur => Component.OnBlur;

        public EventCallback<FocusEventArgs> OnFocus => Component.OnFocus;

        public EventCallback<KeyboardEventArgs> OnKeyDown => Component.OnKeyDown;

        public Func<ChangeEventArgs, Task> HandleOnChange => Component.HandleOnChange;

        public Func<FocusEventArgs, Task> HandleOnBlur => Component.HandleOnBlur;

        public Func<ChangeEventArgs, Task> HandleOnInput => Component.HandleOnInput;

        public Func<FocusEventArgs, Task> HandleOnFocus => Component.HandleOnFocus;

        public Func<KeyboardEventArgs, Task> HandleOnKeyDown => Component.HandleOnKeyDown;
    }
}
