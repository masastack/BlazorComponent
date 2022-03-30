using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSelectableInput<TInput> where TInput : ISelectable
    {
        [Parameter]
        public string Type { get; set; }

        public string Id => Component.Id;

        public bool IsDisabled => Component.IsDisabled;

        public bool InternalValue => Component.InternalValue;

        public Dictionary<string, object> InputAttrs => Component.InputAttrs;

        public bool IsActive => Component.IsActive;

        public Func<FocusEventArgs, Task> HandleOnBlur => Component.HandleOnBlur;

        public Func<ChangeEventArgs, Task> HandleOnChange => Component.HandleOnChange;

        public Func<FocusEventArgs, Task> HandleOnFocus => Component.HandleOnFocus;

        public Func<KeyboardEventArgs, Task> HandleOnKeyDown => Component.HandleOnKeyDown;
    }
}
