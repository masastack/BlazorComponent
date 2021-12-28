using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectableInput<TInput> where TInput : ISelectable
    {
        [Parameter]
        public string Type { get; set; }

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
