using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BMarkdown : BDomComponentBase, IMarkdown
    {
        [Parameter]
        public virtual string Value { get; set; }
        [Parameter]
        public virtual string Html { get; set; }
    }
}
