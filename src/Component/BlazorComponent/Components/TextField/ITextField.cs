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
        string AppendOuterIcon { get; }

        RenderFragment AppendOuterContent { get; }

        bool HasCounter { get; }

        StringNumberBoolean Counter { get; }

        string PrependInnerIcon { get; }

        RenderFragment PrependInnerContent { get; }

        bool Outlined { get; }

        string LegendInnerHTML { get; }

        bool ShowLabel { get; }

        string Prefix { get; }

        string Suffix { get; }

        bool Autofocus { get; }

        bool IsFocused { get; }

        bool IsDisabled { get; }

        bool PersistentPlaceholder { get; }

        string Placeholder { get; }

        bool IsReadonly { get; }

        string Type { get; }

        TValue InputValue { get; }

        EventCallback<FocusEventArgs> OnBlur { get; }

        EventCallback<FocusEventArgs> OnFocus { get; }

        EventCallback<KeyboardEventArgs> OnKeyDown { get; }

        bool Clearable { get; }

        bool IsDirty { get; }

        string Tag { get; }

        /// <summary>
        /// This will pass to input and override default settings
        /// </summary>
        Dictionary<string, object> InputAttrs { get; }

        string ClearIcon { get; }

        ElementReference InputElement { set; }

        RenderFragment CounterContent { get; }

        BLabel LabelReference { set; }

        ElementReference PrefixElement { set; }

        ElementReference PrependInnerElement { set; }

        Dictionary<string, object> InputSlotAttrs { get; }

        Task HandleOnPrependInnerClickAsync(MouseEventArgs args);

        Task HandleOnAppendOuterClickAsync(MouseEventArgs args);

        Task HandleOnChangeAsync(ChangeEventArgs args);

        Task HandleOnBlurAsync(FocusEventArgs args);

        Task HandleOnInputAsync(ChangeEventArgs args);

        Task HandleOnFocusAsync(FocusEventArgs args);

        Task HandleOnKeyDownAsync(KeyboardEventArgs args);

        Task HandleOnClearClickAsync(MouseEventArgs args);
    }
}
