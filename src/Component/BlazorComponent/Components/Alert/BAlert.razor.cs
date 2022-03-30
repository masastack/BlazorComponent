using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BAlert : BDomComponentBase, IAlert, IThemeable
    {
        public RenderFragment IconContent { get; protected set; }

        public bool IsShowIcon { get; protected set; }

        [Parameter]
        public string Transition { get; set; }

        [Parameter]
        public Borders Border { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string CloseIcon { get; set; } = "mdi-close-circle";

        [Parameter]
        public virtual string CloseLabel { get; set; } = "Close";

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public virtual bool Dismissible { get; set; }

        [Parameter]
        public string Tag { get; set; } = "div";

        [Parameter]
        public AlertTypes Type { get; set; }

        private bool _value = true;

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
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        public Task HandleOnDismiss(MouseEventArgs args)
        {
            Value = false;
            return Task.CompletedTask;
        }
    }
}