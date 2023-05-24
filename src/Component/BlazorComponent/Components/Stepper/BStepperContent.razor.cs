namespace BlazorComponent
{
    public partial class BStepperContent : BDomComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected virtual bool IsRtl { get; set; }

        protected virtual bool IsVertical { get; set; }

        protected bool? NullableIsActive
        {
            get => GetValue<bool?>();
            set => SetValue(value);
        }

        protected bool IsActive => NullableIsActive is true;

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
