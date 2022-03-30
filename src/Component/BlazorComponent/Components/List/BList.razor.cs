using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BList : BDomComponentBase
    {
        // TODO: add cascading value in Menu
        [CascadingParameter(Name = "IsInMenu")]
        public bool IsInMenu { get; set; }

        // TODO: add cascading value in Nav
        [CascadingParameter(Name = "IsInNav")]
        public bool IsInNav { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        // TODO: bool? _expand
        [Parameter]
        public virtual bool Expand { get; set; }

        [Parameter]
        public bool Linkage { get; set; }

        [Parameter]
        public virtual string Tag { get; set; } = "div";

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

        protected List<BListGroup> Groups { get; } = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Attributes["role"] = IsInNav || IsInMenu ? null : "list";
            Attributes["id"] = Id;
        }

        internal void Register(BListGroup listGroup)
        {
            Groups.Add(listGroup);
        }

        internal void Unregister(BListGroup listGroup)
        {
            Groups.Remove(listGroup);
        }
    }
}