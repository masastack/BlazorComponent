using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BButtonIconSlot<TIcon> : ComponentAbstractBase<TIcon> where TIcon : IIcon
    {
        public string NewChildren => Component.NewChildren;

        public EventCallback<MouseEventArgs> OnClick => Component.OnClick;
    }
}
