using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BButtonContent<TButton> : ComponentAbstractBase<TButton>
        where TButton : IButton
    {
        protected RenderFragment ChildContent => Component.ChildContent;
    }
}