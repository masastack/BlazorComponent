using System;
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

        public ThemeCssBuilder ThemeCssBuilder { get; } = new ThemeCssBuilder();

        protected override Task OnInitializedAsync()
        {
            Console.WriteLine("1234");
            if (Variables.Theme != null)
                HeadJsInterop.InsertAdjacentHTML("beforeend", ThemeCssBuilder.Build());

            return base.OnInitializedAsync();
        }
    }
}
