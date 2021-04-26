using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class BNavigationDrawer
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected CssBuilder ContentCssBuilder { get; } = new CssBuilder();

        protected CssBuilder BorderCssBuilder { get; } = new CssBuilder();

        public virtual List<BListItem> ListItems { get; set; }

        public virtual void Select(BListItem selectItem)
        {

        }
    }
}
