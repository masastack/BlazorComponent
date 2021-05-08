using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BChip:BDomComponentBase
    {
        protected bool Show { get; set; } = true;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Close { get; set; }

        [Parameter]
        public string CloseIcon { get; set; }
    }
}
