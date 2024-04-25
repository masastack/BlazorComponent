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
