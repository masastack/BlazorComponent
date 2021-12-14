using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BAlertContent<TAlert> : ComponentPartBase<TAlert>
        where TAlert : IAlert
    {
        protected RenderFragment ChildContent => Component.ChildContent;
    }
}