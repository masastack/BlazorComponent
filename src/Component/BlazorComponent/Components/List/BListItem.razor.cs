using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        protected virtual async Task HandleOnClick(MouseEventArgs args)
        {
            if (Click.HasDelegate)
            {
                await Click.InvokeAsync(args);
            }
        }
    }
}
