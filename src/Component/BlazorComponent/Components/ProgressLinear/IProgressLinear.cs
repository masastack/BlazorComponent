using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IProgressLinear : IHasProviderComponent
    {
        bool Stream { get; }

        double Value { get; }

        RenderFragment<double> ChildContent { get; }
    }
}
