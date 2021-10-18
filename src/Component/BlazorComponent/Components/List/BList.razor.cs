using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BList : BDomComponentBase
    {
        public List<BListItem> Items { get; } = new();

        protected List<BListGroup> Groups { get; } = new();

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
        public virtual string Tag { get; set; } = "div";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Attributes["role"] = IsInNav || IsInMenu ? null : "list";
            Attributes["id"] = Id;
        }

        internal void Register(BListGroup listGroup)
        {
            Groups.Add(listGroup);

            StateHasChanged();
        }

        internal void Unregister(BListGroup listGroup)
        {
            Groups.Remove(listGroup);

            StateHasChanged();
        }

        internal void ListClick(string id)
        {
            if (Expand) return;

            foreach (var group in Groups)
            {
                group.Toggle(id);
            }
            
            StateHasChanged();
        }

        public virtual void Select(BListItem mListItem)
        {
        }
    }
}