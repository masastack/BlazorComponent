using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BButtonIconSlot<TIcon> : ComponentPartBase<TIcon> where TIcon : IIcon
    {
        public string NewChildren => Component.NewChildren;

        public EventCallback<MouseEventArgs> OnClick => Component.OnClick;

        public ElementReference Ref
        {
            get => Ref;
            set => Component.Ref = value;
        }

        public IDictionary<string, object> Attrs => Component.Attrs;

        public bool? IfElse => Component.IfElse;

        public bool ComputedIfElse => IfElse ?? true;
    }
}