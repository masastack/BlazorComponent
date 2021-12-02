using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BDialogContent<TDialog> where TDialog : IDialog
    {
        public ElementReference ContentRef
        {
            set { Component.ContentRef = value; }
        }

        public Dictionary<string, object> ContentAttrs => Component.ContentAttrs;

        public bool ShowContent => Component.ShowContent;

        public bool Value => Component.Value;

        protected async Task HandleOnKeyDown(KeyboardEventArgs args)
        {
            await Component.Keydown(args);
        }
    }
}