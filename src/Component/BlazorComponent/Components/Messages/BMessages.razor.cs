using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BMessages : BDomComponentBase, IMessages
    {
        [Parameter]
        public List<string> Value { get; set; }

        [Parameter]
        public RenderFragment<string> ChildContent{ get; set; }
    }
}
