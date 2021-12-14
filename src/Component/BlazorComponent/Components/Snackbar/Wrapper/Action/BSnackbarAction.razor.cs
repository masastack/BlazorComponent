using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSnackbarAction<TSnackbar> where TSnackbar : ISnackbar
    {
        public string Action => Component.Action;

        public RenderFragment ActionContent => Component.ActionContent;
    }
}
