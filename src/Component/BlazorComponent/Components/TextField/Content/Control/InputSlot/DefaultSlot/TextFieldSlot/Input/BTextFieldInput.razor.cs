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
        public TValue InputValue => Component.InputValue;

        public bool Autofocus => Component.Autofocus;

        public bool Disabled => Component.IsDisabled;

        public bool HasLabel => Component.HasLabel;

        public string Placeholder => (Component.PersistentPlaceholder || Component.IsFocused || !HasLabel) ? Component.Placeholder : null;

        public bool Readonly => Component.IsReadonly;

        public string Id => Component.Id;

        public string InputTag => Component.Tag;

        public Dictionary<string, object> InputAttrs => Component.InputAttrs;

        public EventCallback<ChangeEventArgs> HandleOnChange => EventCallback.Factory.Create<ChangeEventArgs>(Component, Component.HandleOnChangeAsync);

        public EventCallback<FocusEventArgs> HandleOnBlur => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnBlurAsync);

        public EventCallback<ChangeEventArgs> HandleOnInput => EventCallback.Factory.Create<ChangeEventArgs>(Component, Component.HandleOnInputAsync);

        public EventCallback<FocusEventArgs> HandleOnFocus => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnFocusAsync);

        public EventCallback<KeyboardEventArgs> HandleOnKeyDown => EventCallback.Factory.Create<KeyboardEventArgs>(Component, Component.HandleOnKeyDownAsync);
    }
}
