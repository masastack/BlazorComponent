using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BInput : BDomComponentBase
    {
        [Parameter]
        public RenderFragment Prepend { get; set; }

        [Parameter]
        public RenderFragment Append { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Clearable { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> ClearClick { get;set; }

        /// <summary>
        /// 附加图标
        /// </summary>
        [Parameter]
        public string AppendIcon { get; set; }

        protected List<string> Messages { get; set; } = new();

        protected bool Blur { get; set; }

        protected bool ShowDetails => Messages?.Count > 0;

        protected virtual Task HandleClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
