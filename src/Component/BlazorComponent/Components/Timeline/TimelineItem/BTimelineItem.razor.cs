namespace BlazorComponent
{
    public partial class BTimelineItem : BDomComponentBase
    {
        [CascadingParameter]
        protected BTimeline? BTimeline { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public RenderFragment? OppositeContent { get; set; }

        [Parameter]
        public RenderFragment? IconContent { get; set; }

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
    }
}
