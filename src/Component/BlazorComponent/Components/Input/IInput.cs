using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IInput : IAbstractComponent
    {
        RenderFragment AppendContent { get; }

        string AppendIcon { get; }

        RenderFragment ChildContent { get; }

        string Label { get; }

        RenderFragment LabelContent { get; }

        bool HasLabel { get; }

        bool ShowDetails { get; }

        RenderFragment PrependContent { get; }

        string PrependIcon { get; }

        ElementReference InputSlotRef { get; set; }

        EventCallback<MouseEventArgs> OnClick { get; }

        EventCallback<MouseEventArgs> OnMouseUp { get; }

        EventCallback<MouseEventArgs> OnMouseDown { get; }

        bool HasMouseDown { get; set; }

        Task HandleOnClick(MouseEventArgs args);

        Task HandleOnMouseDown(MouseEventArgs args);

        Task HandleOnMouseUp(MouseEventArgs args);
    }
}
