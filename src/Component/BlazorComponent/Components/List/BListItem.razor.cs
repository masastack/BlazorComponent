using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class BListItem : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Href { get; set; }

        public virtual bool IsClickable { get; }

        public virtual Task HandleOnClick(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
