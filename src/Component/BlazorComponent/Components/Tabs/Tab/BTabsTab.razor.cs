using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTabsTab<TTabs> : ComponentPartBase<TTabs>
        where TTabs : ITabs
    {
        protected RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
