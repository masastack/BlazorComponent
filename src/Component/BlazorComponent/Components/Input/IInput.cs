using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IInput : IComponent, IHasProvider
    {
        RenderFragment AppendContent { get; }

        string AppendIcon { get; }

        RenderFragment ChildContent { get; }

        string Label { get; }

        RenderFragment LabelContent { get; }

        bool HasLabel { get; }

        bool ShowDetails { get; }
    }
}
