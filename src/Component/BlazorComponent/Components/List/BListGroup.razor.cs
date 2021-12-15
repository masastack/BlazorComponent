using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorComponent
{
    public partial class BListGroup : BDomComponentBase
    {
        private const string PREPEND = "prepend";
        private const string APPEND = "append";

        private bool _isActive;
        private bool _value;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public BList List { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string Group { get; set; }

        [Parameter]
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                _isActive = value;
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

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (List != null)
            {
                List.Register(this);
            }

            if (Group != null)
            {
                IsActive = MatchRoute(NavigationManager.Uri);
            }

            NavigationManager.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (Group == null) return;

            IsActive = MatchRoute(e.Location);
        }

        private bool MatchRoute(string path)
        {
            var relativePath = NavigationManager.ToBaseRelativePath(path);
            return Group.Contains(relativePath, StringComparison.OrdinalIgnoreCase);
        }

        public void Toggle(string id)
        {
            IsActive = Id == id;
            _ = UpdateValue(IsActive);
        }

        public void HandleOnClick(EventArgs args)
        {
            if (Disabled) return;

            IsActive = !IsActive;
            _ = UpdateValue(IsActive);
        }

        protected override void Dispose(bool disposing)
        {
            List?.Unregister(this);

            if (NavigationManager != null)
            {
                NavigationManager.LocationChanged -= OnLocationChanged;
            }

            base.Dispose(disposing);
        }
    }
}