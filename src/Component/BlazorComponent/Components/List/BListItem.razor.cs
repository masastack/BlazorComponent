using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BListItem : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Href { get; set; }

        [CascadingParameter]
        protected BListItemGroup Group { get; set; }

        private bool _link;

        [Parameter]
        public bool Link
        {
            get
            {
                return _link || (Group != null);
            }
            set
            {
                _link = value;
            }
        }

        private string _key;

        [Parameter]
        public string Key
        {
            get
            {
                return _key == null ? Id : _key;
            }
            set
            {
                _key = value;
            }
        }

        [Obsolete("Use OnClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override void OnParametersSet()
        {
            if (Click.HasDelegate)
            {
                OnClick = Click;
            }
        }

        protected virtual async Task HandleOnClick(MouseEventArgs args)
        {
            if (args.Button == 0)
            {
                if (OnClick.HasDelegate)
                {
                    await OnClick.InvokeAsync(args);
                }
            }
        }
    }
}
