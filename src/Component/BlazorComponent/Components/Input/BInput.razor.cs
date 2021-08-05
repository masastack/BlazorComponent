using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using OneOf;

namespace BlazorComponent
{
    public partial class BInput : BDomComponentBase, IInput
    {
        [Obsolete("Use ApendContent instead.")]
        [Parameter]
        public RenderFragment Append { get; set; }

        [Parameter]
        public RenderFragment AppendContent { get; set; }

        [Parameter]
        public string AppendIcon { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string PrependIcon { get; set; }

        [Parameter]
        public RenderFragment LabelContent { get; set; }

        [Obsolete("Use PrependContent instead.")]
        [Parameter]
        public RenderFragment Prepend { get; set; }

        [Parameter]
        public RenderFragment PrependContent { get; set; }

        [Parameter]
        public StringBoolean HideDetails { get; set; } = false;

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnMouseDown { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnMouseUp { get; set; }

        protected List<string> Messages { get; set; } = new();

        protected ElementReference InputSlotRef { get; set; }

        //TODO:等待menu重构
        public Func<MouseEventArgs, Task> OnExtraClick { get; set; }

        protected bool HasMouseDown { get; set; }

        public virtual bool HasLabel => LabelContent != null || Label != null;

        public virtual bool HasDetails => Messages?.Count > 0;

        public virtual bool ShowDetails => HideDetails == false || (HideDetails == "auto" && HasDetails);

        protected override void OnParametersSet()
        {
            if (Prepend != null)
            {
                PrependContent = Prepend;
            }

            if (Append != null)
            {
                AppendContent = Append;
            }
        }

        protected virtual async Task HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }

            //TODO:menu的设计需要改进
            if (OnExtraClick != null)
            {
                await OnExtraClick(args);
            }
        }

        protected virtual async Task HandleOnMouseDown(MouseEventArgs args)
        {
            HasMouseDown = true;
            if (OnMouseDown.HasDelegate)
            {
                await OnMouseDown.InvokeAsync(args);
            }
        }

        protected virtual async Task HandleOnMouseUp(MouseEventArgs args)
        {
            HasMouseDown = false;
            if (OnMouseUp.HasDelegate)
            {
                await OnMouseUp.InvokeAsync(args);
            }
        }
    }
}
