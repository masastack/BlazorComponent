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

        protected virtual string ComputedTransition { get; }

        protected virtual Task OnLeave(Element element)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnEnterTo(Element element)
        {
            return Task.CompletedTask;
        }
    }
}