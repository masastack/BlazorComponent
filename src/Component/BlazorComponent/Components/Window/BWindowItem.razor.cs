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

        protected bool ShowContent { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!ShowContent && InternalIsActive)
            {
                InternalIsActive = false;
                ShowContent = true;
                await InvokeStateHasChangedAsync();

                InternalIsActive = true;
                await InvokeStateHasChangedAsync();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (InternalIsActive)
            {
                ShowContent = true;
            }
        }

        protected virtual string ComputedTransition { get; }

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