using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BFontIconSlot<TIcon> : ComponentAbstractBase<TIcon> where TIcon : IIcon
    {
        public bool Disabled => Component.Disabled;

        public string Tag => Component.Tag;

        public string NewChildren => Component.NewChildren;

        public EventCallback<MouseEventArgs> OnClick => Component.OnClick;
    }
}
