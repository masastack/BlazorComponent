using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IPicker : IHasProviderComponent
    {
        RenderFragment TitleContent { get; }

        RenderFragment ActionsContent { get; }

        string Transition { get; }

        RenderFragment ChildContent { get; }

        bool NoTitle { get; }
    }
}
