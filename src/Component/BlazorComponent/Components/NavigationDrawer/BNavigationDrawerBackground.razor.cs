using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BNavigationDrawerBackground<TNavigationDrawer> where TNavigationDrawer : INavigationDrawer
    {
        public RenderFragment<Dictionary<string, object>> ImgContent => Component.ImgContent;

        public Dictionary<string, object> ImgProps => new()
        {
            { "Height", "100%" },
            { "Width", "100%" },
            { "Src", Component.Src }
        };
    }
}
