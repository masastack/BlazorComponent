using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ITimelineItem : IHasProviderComponent
    {
        RenderFragment ChildContent { get; }

        bool HideDot => default;

        string Icon => default;

        RenderFragment IconContent { get; }

        RenderFragment OppositeContent { get; }
    }
}
