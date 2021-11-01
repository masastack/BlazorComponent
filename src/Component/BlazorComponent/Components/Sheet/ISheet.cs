using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ISheet : IHasProviderComponent
    {
        RenderFragment ChildContent { get; }
    }
}
