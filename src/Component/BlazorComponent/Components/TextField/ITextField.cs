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
                throw new NotImplementedException();
            }
        }

        RenderFragment AppendOuterContent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool HasCounter => false;

        string PrependInnerIcon
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        RenderFragment PrependInnerContent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool Outlined => false;

        string LegendInnerHTML
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ShowLabel => false;

        string Prefix => null;

        string Suffix => null;

        bool Autofocus => false;

        bool IsFocused => false;

        bool IsDisabled => false;

        bool PersistentPlaceholder => false;

        string Placeholder => null;

        bool IsReadonly => false;

        string Type => "text";

        EventCallback<FocusEventArgs> OnBlur
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        EventCallback<FocusEventArgs> OnFocus
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        EventCallback<KeyboardEventArgs> OnKeyDown
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool Clearable => false;

        bool IsDirty => false;

        StringBoolean Loading => false;

        string Tag => "input";

        /// <summary>
        /// This will pass to input and override default settings
        /// </summary>
        Dictionary<string, object> InputAttrs => new();

        RenderFragment ProgressContent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string ClearIcon
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        ElementReference InputRef
        {
            get
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
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

        Task HandleOnClear(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
