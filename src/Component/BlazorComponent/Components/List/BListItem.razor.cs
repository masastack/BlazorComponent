using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorComponent
{
    public partial class BListItem : BGroupItem<ItemGroupBase>, IRoutable, ILinkable
    {
        private Linker _linker;
        private IRoutable _router;

        public BListItem() : base(GroupType.ListItemGroup)
        {
        }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter(Name = "IsInGroup")]
        public bool IsInGroup { get; set; }

        [CascadingParameter(Name = "IsInMenu")]
        public bool IsInMenu { get; set; }

        [CascadingParameter(Name = "IsInList")]
        public bool IsInList { get; set; }

        [CascadingParameter(Name = "IsInNav")]
        public bool IsInNav { get; set; }

        [CascadingParameter]
        public BList List { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Exact { get; set; }

        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public RenderFragment<ItemContext> ItemContent { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public bool Linkage { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public string Tag { get; set; } = "div";

        [Parameter]
        public string Target { get; set; }

        protected RenderFragment ComputedItemContent => ItemContent(GenItemContext());

        public bool IsClickable => _router.IsClickable || ItemGroup != null;

        public bool IsLink => _router.IsLink;

        public bool IsLinkage => Href != null && (List?.Linkage ?? Linkage);

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _linker = new Linker(this);

            NavigationManager.LocationChanged += OnLocationChanged;

            UpdateActiveForLinkage();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _linker = new Linker(this);

            _router = new Router(this);
            (Tag, Attributes) = _router.GenerateRouteLink();

            SetAttrs();
        }


        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            UpdateActiveForLinkage();

            StateHasChanged();
        }

        protected virtual async Task HandleOnClick(MouseEventArgs args)
        {
            if (args.Detail > 0)
            {
                await JsInvokeAsync(JsInteropConstants.Blur, Ref);
            }

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }

            await ToggleAsync();
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
                Toggle = ToggleAsync,
                Ref = RefBack,
                Value = Value
            };
        }

        private void UpdateActiveForLinkage()
        {
            if (IsLinkage)
            {
                IsActive = _linker.MatchRoute(Href);
            }
        }

        protected override void Dispose(bool disposing)
        {
            NavigationManager.LocationChanged -= OnLocationChanged;

            base.Dispose(disposing);
        }
    }
}