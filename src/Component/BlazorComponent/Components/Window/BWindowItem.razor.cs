using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BWindowItem : BGroupItem<ItemGroupBase>
    {
        public BWindowItem() : base(GroupType.Window)
        {
        }

        private bool ShowContent { get; set; }

        protected virtual string ComputedTransition { get; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await ShowLazyContent();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (InternalIsActive)
            {
                ShowContent = true;
            }
        }

        protected virtual async Task ShowLazyContent()
        {
            if (!ShowContent && InternalIsActive)
            {
                InternalIsActive = false;
                ShowContent = true;

                // Make sure the html for content is loaded
                StateHasChanged();
                await Task.Delay(BROWSER_RENDER_INTERVAL);

                InternalIsActive = true;
                StateHasChanged();
            }
        }

        protected virtual Task OnBeforeTransition()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnAfterTransition()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnEnterTo()
        {
            return Task.CompletedTask;
        }
    }
}