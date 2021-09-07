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
    /// <summary>
    ///  &lt;AbstractComponent Metadata="AbstractProvider.GetMetadata(typeof(CascadingValue&lt;&gt;))"&gt;<br/>
    ///  --&lt;div class="@CssProvider.GetClass()" style="@CssProvider.GetStyle()" id="@Id" @ref="Ref"&gt;<br/>
    ///  ----&lt;AbstractComponent Metadata="AbstractProvider.GetMetadata(typeof(BInputContent&lt;,&gt;))"&gt;<br/>
    ///  ----&lt;/AbstractComponent&gt;<br/>
    ///  --&lt;/div&gt;<br/>
    ///  &lt;/AbstractComponent&gt;
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public partial class BInput<TValue> : BDomComponentBase, IInput<TValue>
    {
        [Obsolete("Use ApendContent instead.")]
        [Parameter]
        public RenderFragment Append { get; set; }

        [Parameter]
        public RenderFragment AppendContent { get; set; }

        [Parameter]
        public virtual string AppendIcon { get; set; }

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
        public string Hint { get; set; }

        [Parameter]
        public bool PersistentHint { get; set; }

        [Parameter]
        public StringBoolean Loading { get; set; } = false;

        [Parameter]
        public RenderFragment<string> MessageContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnMouseDown { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnMouseUp { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnPrependClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnAppendClick { get; set; }

        public ElementReference InputSlotRef { get; set; }

        protected bool HasMouseDown { get; set; }

        public virtual bool HasLabel => LabelContent != null || Label != null;

        public virtual bool HasDetails => MessagesToDisplay.Count > 0;

        public virtual bool ShowDetails => HideDetails == false || (HideDetails == "auto" && HasDetails);

        public virtual bool HasHint => !HasError && !string.IsNullOrEmpty(Hint) && (PersistentHint || IsFocused);

        public virtual List<string> MessagesToDisplay
        {
            get
            {
                if (HasHint)
                {
                    return new List<string>
                    {
                        Hint
                    };
                }

                return ErrorMessages;
            }
        }

        public virtual async Task HandleOnPrependClickAsync(MouseEventArgs args)
        {
            if (OnPrependClick.HasDelegate)
            {
                await OnPrependClick.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnAppendClickAsync(MouseEventArgs args)
        {
            if (OnAppendClick.HasDelegate)
            {
                await OnAppendClick.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnMouseDownAsync(MouseEventArgs args)
        {
            HasMouseDown = true;
            if (OnMouseDown.HasDelegate)
            {
                await OnMouseDown.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnMouseUpAsync(MouseEventArgs args)
        {
            HasMouseDown = false;
            if (OnMouseUp.HasDelegate)
            {
                await OnMouseUp.InvokeAsync(args);
            }
        }

        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply(typeof(CascadingValue<>), typeof(CascadingValue<BInput<TValue>>), props =>
                 {
                     props[nameof(CascadingValue<BInput<TValue>>.Value)] = this;
                 });
        }
    }
}
