using Microsoft.AspNetCore.Components;

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
