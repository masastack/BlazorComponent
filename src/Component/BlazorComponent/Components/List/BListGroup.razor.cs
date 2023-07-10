using Microsoft.AspNetCore.Components.Routing;
using System.Text.RegularExpressions;

namespace BlazorComponent
{
    public partial class BListGroup : BDomComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [CascadingParameter]
        public BList? List { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public List<string>? Group { get; set; }

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

        [Parameter]
        public string? PrependIcon { get; set; }

        [Parameter]
        public RenderFragment? PrependIconContent { get; set; }

        [Parameter]
        public string? AppendIcon { get; set; }

        [Parameter]
        public RenderFragment? AppendIconContent { get; set; }

        [Parameter]
        public RenderFragment? ActivatorContent { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool SubGroup { get; set; }

        [Parameter]
        public bool Eager { get; set; }

        private const string PREPEND = "prepend";
        private const string APPEND = "append";

        private bool _value;
        private string? _previousAbsolutePath;

        protected bool IsActive { get; set; }

        protected bool IsBooted { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            List?.Register(this);

            NavigationManager.LocationChanged += OnLocationChanged;

            _previousAbsolutePath = NavigationManager.GetAbsolutePath();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                var shouldRender = UpdateActiveForRoutable();
                if (shouldRender)
                {
                    StateHasChanged();
                }
            }
        }

        protected override void OnParametersSet()
        {
            EnsureBooted();
        }

        private void EnsureBooted()
        {
            if (!IsBooted && (IsActive || Eager))
            {
                IsBooted = true;
            }
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            var absolutePath = NavigationManager.GetAbsolutePath();
            if (_previousAbsolutePath == absolutePath)
            {
                return;
            }

            _previousAbsolutePath = absolutePath;

            var shouldRender = UpdateActiveForRoutable();
            if (shouldRender)
            {
                InvokeStateHasChanged();
            }
        }

        private async Task HandleOnClick(EventArgs args)
        {
            if (Disabled) return;

            if (!IsBooted)
            {
                IsBooted = true;

                // waiting for one frame(16ms) to make sure the element has been rendered,
                await Task.Delay(16);

                StateHasChanged();
            }

            IsActive = !IsActive;
            await UpdateValue(IsActive);
        }

        private bool MatchRoute(string path)
        {
            if (Group is null) return false;

            var relativePath = "/" + NavigationManager.ToBaseRelativePath(path);
            return Group.Any(item => Regex.Match(relativePath, item, RegexOptions.IgnoreCase).Success);
        }

        private bool UpdateActiveForRoutable()
        {
            var isActive = IsActive;

            if (Group != null)
            {
                IsActive = MatchRoute(NavigationManager.Uri);
                EnsureBooted();
            }

            return isActive != IsActive;
        }

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

        protected override void Dispose(bool disposing)
        {
            List?.Unregister(this);

            NavigationManager.LocationChanged -= OnLocationChanged;

            base.Dispose(disposing);
        }
    }
}
