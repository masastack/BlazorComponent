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

        [CascadingParameter(Name = "IsInGroup")]
        public bool IsInGroup { get; set; }

        [CascadingParameter(Name = "IsInMenu")]
        public bool IsInMenu { get; set; }

        [CascadingParameter(Name = "IsInList")]
        public bool IsInList { get; set; }

        [CascadingParameter(Name = "IsInNav")]
        public bool IsInNav { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string Href { get; set; }
        
        [Parameter]
        public RenderFragment<ItemContext> ItemContent { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected RenderFragment ComputedItemContent => ItemContent(GenItemContext());

        public bool IsLink => Href != null || Link;

        public bool IsClickable
        {
            get
            {
                if (Disabled) return false;

                if (ItemGroup != null) return true;

                return IsLink || OnClick.HasDelegate || (Attributes.TryGetValue("tabindex", out var tabIndex) && Convert.ToInt32(tabIndex) > -1);
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            
            SetAttrs();
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

        private void SetAttrs()
        {
            Attributes["aria-disabled"] = Disabled ? true : null;
            Attributes["tabindex"] = IsClickable ? 0 : -1;

            if (Attributes.ContainsKey("role"))
            {
                // do nothing, role already provided
            }
            else if (IsInNav)
            {
                // do nothing, role is inherit (TODO:check)
            }
            else if (IsInGroup)
            {
                Attributes["role"] = "option";
                Attributes["aria-selected"] = IsActive.ToString();
            }
            else if (IsInMenu)
            {
                Attributes["role"] = IsClickable ? "menuitem" : null;
                Attributes["id"] = Id ?? $"list-item-{Id}"; // TODO:check
            }
            else if (IsInList)
            {
                Attributes["role"] = "listitem";
            }
        }

        private ItemContext GenItemContext()
        {
            return new ItemContext()
            {
                Active = IsActive,
                ActiveClass = IsActive ? ComputedActiveClass : "",
                Toggle = ToggleItem,
                Ref = RefBack,
                Value = Value
            };
        }
    }
}