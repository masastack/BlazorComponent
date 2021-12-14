using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BButtonLoader<TButton> : ComponentPartBase<TButton>
        where TButton : IButton
    {
        protected bool Loading => Component.Loading;

        protected RenderFragment LoaderContent => Component.LoaderContent;
    }
}