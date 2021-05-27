using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCascaderSelectBody : BDomComponentBase, ISelectBody
    {
        protected bool ShowSubItems { get; set; }

        protected List<BCascaderNode> SubItems { get; set; }

        [Parameter]
        public List<BCascaderNode> Items { get; set; }
    }
}
