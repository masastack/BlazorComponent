using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BButtonIconSlot<TIcon> : ComponentAbstractBase<TIcon> where TIcon : IIcon
    {
        public string NewChildren => Component.NewChildren;

        public EventCallback<MouseEventArgs> OnClick => Component.OnClick;

        public ElementReference Ref
        {
            get => Ref;
            set => Component.Ref = value;
        }

        public IDictionary<string, object> Attrs => Component.Attrs;
    }
}