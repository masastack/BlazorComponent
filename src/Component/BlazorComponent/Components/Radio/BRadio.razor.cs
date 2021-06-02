using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BRadio<TValue>
    {
        [Parameter]
        public bool IsActive { get; set; }

        [Parameter]
        public string OnIcon { get; set; }

        [Parameter]
        public string OffIcon { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        [Parameter]
        public bool IsReadonly { get; set; }

        [Parameter]
        public TValue Value { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnBlur { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnChange { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnFocus { get; set; }

        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

        [Parameter]
        public RenderFragment LabelContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Ripple { get; set; } = true;

        public event Func<BRadio<TValue>, Task> NotifyChange;

        protected virtual async Task HandleClick(MouseEventArgs args)
        {
            if (IsDisabled || IsReadonly || IsActive)
            {
                return;
            }

            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(new ChangeEventArgs { Value = Value });
            }

            await NotifyChange?.Invoke(this);
        }

        public void Active()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            StateHasChanged();
        }

        public void DeActive()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;
            StateHasChanged();
        }
    }
}
