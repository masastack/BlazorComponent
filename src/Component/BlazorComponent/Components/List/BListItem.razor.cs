using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BListItem : BGroupItem<ItemGroupBase>
    {
        public BListItem() : base(GroupType.ListItemGroup)
        {

        }

        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public string Color { get; set; }

        private bool _link;

        [Parameter]
        public bool Link
        {
            get
            {
                return _link || (ItemGroup != null);
            }
            set
            {
                _link = value;
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
                await ToggleItem();

                if (OnClick.HasDelegate)
                {
                    await OnClick.InvokeAsync(args);
                }
            }
        }
    }
}
