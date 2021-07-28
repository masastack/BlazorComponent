using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;

namespace BlazorComponent
{
    public partial class BInput : BDomComponentBase
    {
        [Obsolete("Use PrependContent instead.")]
        [Parameter]
        public RenderFragment Prepend { get; set; }

        [Parameter]
        public RenderFragment PrependContent { get; set; }

        [Obsolete("Use ApendContent instead.")]
        [Parameter]
        public RenderFragment Append { get; set; }

        [Parameter]
        public RenderFragment AppendContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Clearable { get; set; }

        [Obsolete("Use OnClearClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> ClearClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClearClick { get; set; }

        /// <summary>
        /// 附加图标
        /// </summary>
        [Parameter]
        public string AppendIcon { get; set; }

        protected List<string> Messages { get; set; } = new();

        protected bool Blur { get; set; }

        [Parameter]
        public StringBoolean HideDetails { get; set; } = "auto";

        public virtual bool HasDetails => Messages?.Count > 0;

        protected bool ShowDetails => HideDetails == false || (HideDetails == "auto" && HasDetails);

        protected override void OnParametersSet()
        {
            if (ClearClick.HasDelegate)
            {
                OnClearClick = ClearClick;
            }

            if (Prepend != null)
            {
                PrependContent = Prepend;
            }

            if (Append != null)
            {
                AppendContent = Append;
            }
        }

        protected virtual Task HandleClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
