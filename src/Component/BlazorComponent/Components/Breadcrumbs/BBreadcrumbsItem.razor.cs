using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    /// <summary>
    /// The component that can be rendered with <see cref="BBreadcrumbsLinkItem"/> or <see cref="BBreadcrumbsPlainItem"/>.
    /// </summary>
    public abstract partial class BBreadcrumbsItem : BDomComponentBase
    {
        [Parameter]
        public string Tag { get; set; } = "li";

        [Parameter]
        public RenderFragment ChildContent { get; set; }
      
        public (string tag, Dictionary<string, object> props) ChildProps { get; set; }
    }
}