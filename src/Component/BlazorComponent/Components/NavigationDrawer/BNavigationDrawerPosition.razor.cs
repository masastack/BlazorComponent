using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BNavigationDrawerPosition<TNavigationDrawer> where TNavigationDrawer : INavigationDrawer
    {
        [Parameter]
        public string ClassName { get; set; }

        public RenderFragment PositionContent => "prepend".Equals(ClassName) ? 
            Component.PrependContent : Component.AppendContent;
    }
}
