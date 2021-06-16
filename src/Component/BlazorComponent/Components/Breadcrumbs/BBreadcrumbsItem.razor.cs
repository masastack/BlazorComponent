using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract partial class BBreadcrumbsItem : BDomComponentBase
    {
        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public Boolean Disabled { get; set; }
    }
}
