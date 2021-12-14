using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel;

namespace BlazorComponent
{
    public partial class BSvgIconSlot<TIcon> : ComponentPartBase<TIcon> where TIcon : IIcon
    {
        public string Icon => Component.Icon;

        public bool Disabled => Component.Disabled;

        public ElementReference Ref
        {
            get => Ref;
            set => Component.Ref = value;
        }

        public string Tag => Component.Tag;

        public Dictionary<string, object> SvgAttrs => Component.SvgAttrs;

        public EventCallback<MouseEventArgs> OnClick => Component.OnClick;

        public IDictionary<string, object> Attrs => Component.Attrs;
    }
}