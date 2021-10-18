using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BListGroup : BDomComponentBase
    {
        private const string PREPEND = "prepend";
        private const string APPEND = "append";

        [CascadingParameter]
        public BList List { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        private bool _value;

        [Parameter]
        public bool Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                _value = value;
                ValueChanged.InvokeAsync(_value);
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public string PrependIcon { get; set; }

        [Parameter]
        public string AppendIcon { get; set; }

        [Parameter]
        public RenderFragment ActivatorContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool SubGroup { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            List?.Register(this);

            _isActive = Value;
        }

        private bool _isActive;
        private bool _isActiveUpdated;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (!SubGroup && value && !_isActiveUpdated)
                {
                    List?.ListClick(Id);
                }

                _isActive = value;
            }
        }

        public void Toggle(string id)
        {
            _isActiveUpdated = true;
            IsActive = Id == id;
        }

        public void HandleOnClick(EventArgs args)
        {
            if (Disabled) return;

            _isActiveUpdated = false;
            IsActive = !IsActive;
        }

        protected override void Dispose(bool disposing)
        {
            List?.Unregister(this);

            base.Dispose(disposing);
        }
    }
}