using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTooltip : BDomComponentBase
    {
        [Parameter]
        public RenderFragment Activator{ get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public ElementReference ContentRef { get; set; }

        protected bool IsActive { get; set; }
    }
}
