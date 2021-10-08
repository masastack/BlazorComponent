using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public interface IProgressLinear : IHasProviderComponent
    {
        bool Stream { get; }

        double Value { get; }

        RenderFragment<double> ChildContent { get; }
    }
}
