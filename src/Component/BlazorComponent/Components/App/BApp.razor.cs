using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OneOf;

namespace BlazorComponent
{
    public abstract partial class BApp : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        protected HeadJsInterop HeadJsInterop { get; set; }

        protected ThemeCssBuilder ThemeCssBuilder { get; } = new ThemeCssBuilder();

        protected override Task OnInitializedAsync()
        {
            if (Variables.Theme != null)
                HeadJsInterop.InsertAdjacentHTML("beforeend", ThemeCssBuilder.Build());

            return base.OnInitializedAsync();
        }

    }
}
