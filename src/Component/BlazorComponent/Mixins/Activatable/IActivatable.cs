using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IActivatable
    {
        Dictionary<string, object> ActivatorAttributes { get; }

        bool IsActive { get; }

        RenderFragment ComputedActivatorContent { get; }
    }
}