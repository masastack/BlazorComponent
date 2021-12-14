using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BAlertIcon<TAlert> : ComponentPartBase<TAlert>
        where TAlert : IAlert
    {
        protected RenderFragment IconContent => Component.IconContent;
    }
}