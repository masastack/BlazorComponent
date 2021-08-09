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
        public TValue Value => Component.Value;

        public bool Autofocus => Component.Autofocus;

        public bool IsDisabled => Component.IsDisabled;

        public bool HasLabel => Component.HasLabel;

        public string Placeholder => (Component.PersistentPlaceholder || Component.IsFocused || !HasLabel) ? Component.Placeholder : null;

        public bool Readonly => Component.IsReadonly;

        public string InputTag => Component.Tag;

        public Dictionary<string, object> InputAttrs => Component.InputAttrs;

        public EventCallback<ChangeEventArgs> HandleOnChange => EventCallback.Factory.Create<ChangeEventArgs>(Component, Component.HandleOnChange);

        public EventCallback<FocusEventArgs> HandleOnBlur => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnBlur);

        public EventCallback<ChangeEventArgs> HandleOnInput => EventCallback.Factory.Create<ChangeEventArgs>(Component, Component.HandleOnInput);

        public EventCallback<FocusEventArgs> HandleOnFocus => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnFocus);

        public EventCallback<KeyboardEventArgs> HandleOnKeyDown => EventCallback.Factory.Create<KeyboardEventArgs>(Component, Component.HandleOnKeyDown);
    }
}
