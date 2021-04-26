using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTableFooter
    {
        public const string PREV = "prev";
        public const string NEXT = "next";

        [Parameter]
        public RenderFragment Select { get; set; }

        [Parameter]
        public int PageStart { get; set; }

        [Parameter]
        public int PageStop { get; set; }

        [Parameter]
        public int ItemsLength { get; set; }
    }
}
