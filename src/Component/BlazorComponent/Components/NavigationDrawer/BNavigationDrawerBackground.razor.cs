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
        public NavigationDrawerImgProps Props => new("100%", "100%", Component.Src);

        public RenderFragment<NavigationDrawerImgProps> ImgContent => Component.ImgContent;
    }
}
