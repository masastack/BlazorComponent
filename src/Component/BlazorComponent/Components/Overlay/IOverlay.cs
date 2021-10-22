using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IOverlay : IHasProviderComponent
    {
        bool Value { get; }

        RenderFragment ChildContent { get; }
    }
}
