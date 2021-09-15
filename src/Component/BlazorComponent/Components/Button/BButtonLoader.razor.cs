using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BButtonLoader<TButton> : ComponentAbstractBase<TButton>
        where TButton : IButton
    {
        protected bool Loading => Component.Loading;

        protected RenderFragment LoaderContent => Component.LoaderContent;
    }
}