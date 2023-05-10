namespace BlazorComponent
{
    public partial class BSelectableInput<TInput, TValue> where TInput : ISelectable<TValue>
    {
        [Parameter, EditorRequired]
        public string Type { get; set; } = null!;

        public string? Id => Component.Id;

        public bool IsDisabled => Component.IsDisabled;

        public TValue? InternalValue => Component.InternalValue;

        public Dictionary<string, object> InputAttrs => Component.InputAttrs;

        public bool IsActive => Component.IsActive;

        public EventCallback<FocusEventArgs> HandleOnBlur =>  EventCallback.Factory.Create<FocusEventArgs>(Component,Component.HandleOnBlur);

        public EventCallback HandleOnChange => EventCallback.Factory.Create(Component, Component.HandleOnChange);

        public EventCallback<FocusEventArgs> HandleOnFocus => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnFocus);

        public EventCallback<KeyboardEventArgs> HandleOnKeyDown => EventCallback.Factory.Create<KeyboardEventArgs>(Component, Component.HandleOnKeyDown);
    }
}
