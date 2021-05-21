using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BPicker
    {
        [Parameter]
        public RenderFragment TitleContent { get; set; }

        [Parameter]
        public RenderFragment ActionsContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Type { get; set; } = "date";

        public string ActivePicker => Type.ToUpper();

        [Parameter]
        public bool NoTitle { get; set; }
    }
}
