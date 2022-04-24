using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BFontIconSlot<TIcon> : ComponentPartBase<TIcon> where TIcon : IIcon
    {
        public bool Disabled => Component.Disabled;

        public ElementReference Ref
        {
            get => Ref;
            set => Component.Ref = value;
        }

        public string Tag => Component.Tag;

        public string NewChildren => Component.NewChildren;

        public EventCallback<MouseEventArgs> OnClick => Component.OnClick;

        public IDictionary<string, object> Attrs => Component.Attrs;

        public bool? IfElse => Component.IfElse;

        public bool ComputedIfElse => IfElse ?? true;
    }
}