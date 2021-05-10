using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCascaderSelectSlot : BSelectSlot
    {
        [Parameter]
        public List<BCascaderNode> Items { get; set; }

        [Parameter]
        public BSelect<BCascaderNode> Select { get; set; }
    }
}
