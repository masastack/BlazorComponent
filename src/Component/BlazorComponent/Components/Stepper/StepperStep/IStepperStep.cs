using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IStepperStep : IHasProviderComponent
    {
        int Step { get; }

        RenderFragment ChildContent { get; }

        bool Editable { get; }

        bool Complete { get; }

        bool HasError { get; }

        string ErrorIcon { get; }

        string CompleteIcon { get; }

        string EditIcon { get; }
    }
}
