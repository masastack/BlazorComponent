using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCounter
    {
        [Parameter]
        public StringNumber Value { get; set; } = "";

        [Parameter]
        public StringNumber Max { get; set; }
    }
}
