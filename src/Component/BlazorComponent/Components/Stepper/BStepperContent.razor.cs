namespace BlazorComponent
{
    public partial class BStepperContent : BDomComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool Eager { get; set; }

        protected bool IsBooted { get; set; }

        protected virtual bool IsRtl { get; set; }

        protected virtual bool IsVertical { get; set; }

        protected bool? NullableIsActive
        {
            get => GetValue<bool?>();
            set => SetValue(value);
        }

        protected bool IsActive { get; set; }

        protected bool IsReverse { get; set; }

        protected string TransitionName
        {
            get
            {
                var reverse = IsRtl ? !IsReverse : IsReverse;

                return reverse ? "tab-reverse-transition" : "tab-transition";
            }
        }
    }
}
