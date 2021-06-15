using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BIcon : BDomComponentBase
    {
        protected string _icon;
        protected string _css;
        protected IconTag _tag = IconTag.I;

        private string Css
        {
            get
            {
                return CssProvider.GetClass() + " " + _css;
            }
        }

        // TODO: 维护内置颜色列表
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Size { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }
    }
}
