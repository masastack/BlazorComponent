using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BWindowItem : BGroupItem<ItemGroupBase>
    {
        public BWindowItem() : base(GroupType.Window)
        {
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