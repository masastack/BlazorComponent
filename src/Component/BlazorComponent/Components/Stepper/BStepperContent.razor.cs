using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BStepperContent : BDomComponentBase
    {
        protected virtual bool IsRtl { get; set; }

        protected virtual bool IsVertical { get; set; }

        protected bool IsActive
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        protected bool IsReverse { get; set; }

        protected string TransitionName
        {
            get
            {
                var reverse = IsRtl ? !IsReverse : IsReverse;

                return reverse ? "tab-reverse-transition" : "tab-transition";
            }
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
