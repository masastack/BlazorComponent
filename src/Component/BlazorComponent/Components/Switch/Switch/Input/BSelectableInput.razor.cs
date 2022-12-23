using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSelectableInput<TInput, TValue> where TInput : ISelectable<TValue>
    {
        [Parameter]
        public string Type { get; set; }

        public string Id => Component.Id;

        public bool IsDisabled => Component.IsDisabled;

        public TValue InternalValue => Component.InternalValue;

        public Dictionary<string, object> InputAttrs => Component.InputAttrs;

        public bool IsActive => Component.IsActive;

        public EventCallback<FocusEventArgs> HandleOnBlur =>  EventCallback.Factory.Create<FocusEventArgs>(Component,Component.HandleOnBlur);

        public EventCallback HandleOnChange => EventCallback.Factory.Create(Component, Component.HandleOnChange);

        public EventCallback<FocusEventArgs> HandleOnFocus => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnFocus);

        public EventCallback<KeyboardEventArgs> HandleOnKeyDown => EventCallback.Factory.Create<KeyboardEventArgs>(Component, Component.HandleOnKeyDown);
    }
}
