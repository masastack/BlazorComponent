using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBadgeContent<TBadge> where TBadge : IBadge
    {
        public bool Dot => Component.Dot;

        public RenderFragment BadgeContent => Component.BadgeContent;

        public StringNumber Content => Component.Content;

        public string Icon => Component.Icon;
    }
}
