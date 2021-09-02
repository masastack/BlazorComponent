using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BAlertContent<TAlert> : ComponentAbstractBase<TAlert>
        where TAlert : IAlert
    {
        private RenderFragment ChildContent => Component.ChildContent;
    }
}