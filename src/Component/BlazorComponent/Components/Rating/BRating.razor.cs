using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BRating : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment<RatingItem> ItemContent { get; set; }

        [Parameter]
        public StringNumber Length { get; set; } = 5;
    }
}
