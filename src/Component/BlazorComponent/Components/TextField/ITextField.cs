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
    public interface ITextField<TValue> : IInput<TValue>, ILoadable
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

        StringNumberBoolean Counter => default;

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

        string Tag => "input";

        /// <summary>
        /// This will pass to input and override default settings
        /// </summary>
        Dictionary<string, object> InputAttrs => new();

        string ClearIcon
        {
            get
            {
                return default;
            }
        }

        ElementReference InputElement
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

        BLabel LabelReference { set; }

        ElementReference PrefixElement { set; }

        ElementReference PrependInnerElement { set; }

        Task HandleOnAppendOuterClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnChangeAsync(ChangeEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnBlurAsync(FocusEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnInputAsync(ChangeEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnFocusAsync(FocusEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnKeyDownAsync(KeyboardEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnClearClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
