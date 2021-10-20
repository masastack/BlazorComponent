using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface INavigationDrawer : IHasProviderComponent
    {
        RenderFragment PrependContent { get; }

        RenderFragment ChildContent { get; }

        RenderFragment AppendContent { get; }

        RenderFragment<NavigationDrawerImgProps> ImgContent { get; }

        OneOf<string, SrcObject> Src => default;
    }
}
