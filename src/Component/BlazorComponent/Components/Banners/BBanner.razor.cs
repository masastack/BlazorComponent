using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBanner : BDomComponentBase, IHasProviderComponent
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Value { get; set; } = true;

        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply(typeof(CascadingValue<>), typeof(CascadingValue<BBanner>), props =>
                {
                    props[nameof(CascadingValue<BBanner>.Value)] = this;
                });
        }
    }
}
