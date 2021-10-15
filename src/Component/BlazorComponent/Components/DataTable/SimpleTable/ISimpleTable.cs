using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ISimpleTable : IHasProviderComponent
    {
        RenderFragment WrapperContent { get; }

        RenderFragment ChildContent { get; }

        Task HandleOnScrollAsync(EventArgs args);

        ElementReference WrapperElement { set; }
    }
}
