using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IInput<TValue> : IHasProviderComponent
    {
        TValue Value { get; }

        RenderFragment AppendContent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string AppendIcon
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        RenderFragment ChildContent { get; }

        string Label => null;

        RenderFragment LabelContent => null;

        bool HasLabel => false;

        bool ShowDetails => false;

        RenderFragment PrependContent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string PrependIcon
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        ElementReference InputSlotRef
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                //default todo nothing
            }
        }

        Task HandleOnClick(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnMouseDown(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnMouseUp(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
