using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BAlertIcon<TAlert> : ComponentAbstractBase<TAlert>
        where TAlert : IAlert
    {
        private RenderFragment IconContent => Component.IconContent;
    }
}