using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BList:BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public virtual List<BListItem> Items { get; set; }

        public virtual void Select(BListItem mListItem)
        {
            
        }
    }
}
