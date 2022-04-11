using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Text.RegularExpressions;

namespace BlazorComponent
{
    public partial class BListGroup : BDomComponentBase
    {
        private const string PREPEND = "prepend";
        private const string APPEND = "append";
        private bool _value;
        //private bool _booted;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public BList List { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public List<string> Group { get; set; }

        [Parameter]
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                IsActive = value;
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        private async Task UpdateValue(bool value)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }
        }

        [Parameter]
        public string PrependIcon { get; set; }

        [Parameter]
        public RenderFragment PrependIconContent { get; set; }

        [Parameter]
        public string AppendIcon { get; set; }

        [Parameter]
        public RenderFragment AppendIconContent { get; set; }

        [Parameter]
        public RenderFragment ActivatorContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool SubGroup { get; set; }

        protected bool IsActive { get; set; }

        protected bool Booted { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            List?.Register(this);

            NavigationManager.LocationChanged += OnLocationChanged;

            UpdateActiveForLinkage();
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            var shouldRender = UpdateActiveForLinkage();
            if (shouldRender)
            {
                InvokeStateHasChanged();
            }
        }

        internal void Toggle(string id)
        {
            IsActive = Id == id;
            _ = UpdateValue(IsActive);
        }

        private async Task HandleOnClick(EventArgs args)
        {
            if (Disabled) return;

            if (!Booted)
            {
                Booted = true;
                await Task.Delay(BROWSER_RENDER_INTERVAL);
            }

            IsActive = !IsActive;
            await UpdateValue(IsActive);
        }

        private bool MatchRoute(string path)
        {
            var relativePath = "/" + NavigationManager.ToBaseRelativePath(path);
            return Group.Any(item => Regex.Match(relativePath, item, RegexOptions.IgnoreCase).Success);
        }

        private bool UpdateActiveForLinkage()
        {
            var isActive = IsActive;

            if (Group != null)
            {
                IsActive = MatchRoute(NavigationManager.Uri);
            }

            return isActive != IsActive;
        }

        protected override void Dispose(bool disposing)
        {
            List?.Unregister(this);

            NavigationManager.LocationChanged -= OnLocationChanged;

            base.Dispose(disposing);
        }
    }
}