using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTooltip : BDomComponentBase
    {
        [Parameter]
        public string Tag { get; set; } = "span";
    }
}
