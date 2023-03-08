namespace BlazorComponent
{
    public partial class BTab : BRoutableGroupItem<ItemGroupBase>
    {
        public BTab() : base(GroupType.SlideGroup)
        {
        }

        [CascadingParameter]
        public BTabs? Tabs { get; set; }

        protected override bool HasRoutableAncestor => Tabs?.Routable is true;

        protected override bool IsRoutable => Href != null && HasRoutableAncestor;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (Tabs is null) return;

                await Tabs.CallSlider();
            }
        }

        protected override async Task OnActiveUpdatedForRoutable()
        {
            if (Tabs == null) return;

            await Tabs.CallSlider();
        }

        protected override bool AfterHandleEventShouldRender() => false;

        private async Task HandleOnClick(MouseEventArgs args)
        {
            await ToggleAsync();

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }
    }
}
