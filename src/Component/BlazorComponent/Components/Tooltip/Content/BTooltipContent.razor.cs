namespace BlazorComponent
{
    public partial class BTooltipContent<TTooltip> where TTooltip : ITooltip
    {
        public ElementReference ContentElement
        {
            set
            {
                Component.ContentElement = value;
            }
        }

        protected string TransitionName
        {
            get
            {
                if (!string.IsNullOrEmpty(Transition))
                {
                    return Transition;
                }
                else
                {
                    return IsActive ? "scale-transition" : "fade-transition";
                }
            }
        }

        public bool IsActive => Component.IsActive;

        public bool IsBooted => Component.IsBooted;

        public string Transition => Component.Transition;

        public RenderFragment? ComponentChildContent => Component.ChildContent;
    }
}