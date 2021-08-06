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
    public interface ITextField<TValue> : IInput
    {
        string AppendOuterIcon { get; }

        RenderFragment AppendOuterContent { get; }

        bool HasCounter { get; }

        string PrependInnerIcon { get; }

        RenderFragment PrependInnerContent { get; }

        bool Outlined { get; }

        string LegendInnerHTML { get; }

        bool ShowLabel { get; }

        string Prefix { get; }

        string Suffix { get; }

        TValue Value { get; set; }

        EventCallback<TValue> ValueChanged { get; }

        bool Autofocus { get; }

        bool IsDisabled { get; }

        bool PersistentPlaceholder { get; }

        bool IsFocused { get; set; }

        string Placeholder { get; }

        bool IsReadonly { get; }

        string Type { get; }

        EventCallback<FocusEventArgs> OnBlur { get; }

        EventCallback<FocusEventArgs> OnFocus { get; }

        EventCallback<KeyboardEventArgs> OnKeyDown { get; }

        bool Clearable { get; }

        bool IsDirty { get; }

        StringBoolean Loading { get; }

        string Tag { get; }

        Dictionary<string, object> Attrs { get; }

        RenderFragment ProgressContent { get; }

        string ClearIcon { get; }

        ElementReference InputRef { get; set; }

        Task HandleOnChange(ChangeEventArgs args);

        Task HandleOnBlur(FocusEventArgs args);

        Task HandleOnInput(ChangeEventArgs args);

        Task HandleOnFocus(FocusEventArgs args);

        Task HandleOnKeyDown(KeyboardEventArgs args);
    }
}
