using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ITextField<TValue> : IInput<TValue>
    {
        string AppendOuterIcon
        {
            get
            {
                return default;
            }
        }

        RenderFragment AppendOuterContent
        {
            get
            {
                return default;
            }
        }

        bool HasCounter => default;

        Task HandleOnPrependInnerClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        string PrependInnerIcon
        {
            get
            {
                return default;
            }
        }

        RenderFragment PrependInnerContent
        {
            get
            {
                return default;
            }
        }

        bool Outlined => default;

        string LegendInnerHTML
        {
            get
            {
                return default;
            }
        }

        bool ShowLabel => default;

        string Prefix => default;

        string Suffix => default;

        bool Autofocus => default;

        bool IsFocused => default;

        bool IsDisabled => default;

        bool PersistentPlaceholder => default;

        string Placeholder => default;

        bool IsReadonly => default;

        string Type => "text";

        EventCallback<FocusEventArgs> OnBlur
        {
            get
            {
                return default;
            }
        }

        EventCallback<FocusEventArgs> OnFocus
        {
            get
            {
                return default;
            }
        }

        EventCallback<KeyboardEventArgs> OnKeyDown
        {
            get
            {
                return default;
            }
        }

        bool Clearable => default;

        bool IsDirty => default;

        StringBoolean Loading => default;

        string Tag => "input";

        /// <summary>
        /// This will pass to input and override default settings
        /// </summary>
        Dictionary<string, object> InputAttrs => new();

        RenderFragment ProgressContent
        {
            get
            {
                return default;
            }
        }

        string ClearIcon
        {
            get
            {
                return default;
            }
        }

        ElementReference InputRef
        {
            get
            {
                return default;
            }
            set
            {
                //default todo nothing
            }
        }

        RenderFragment CounterContent
        {
            get
            {
                return default;
            }
        }

        Task HandleOnAppendOuterClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnChange(ChangeEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnBlur(FocusEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnInput(ChangeEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnFocus(FocusEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnKeyDown(KeyboardEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnClearClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
