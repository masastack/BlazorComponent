using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class BNavigationDrawer:BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public virtual List<BListItem> ListItems { get; set; }

        public virtual void Select(BListItem selectItem)
        {

        }
    }
}
