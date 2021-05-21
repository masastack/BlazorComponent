using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerHeader : BDomComponentBase
    {
        [Parameter]
        public string PrevIcon { get; set; }

        [Parameter]
        public string NextIcon { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public StringNumber Value { get; set; }

        [Parameter]
        public StringNumber Min { get; set; }

        [Parameter]
        public StringNumber Max { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> PrevClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> NextClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> DateClick { get; set; }
    }
}
