namespace BlazorComponent
{
    public partial class BExpansionPanelContent : BDomComponentBase
    {
        [CascadingParameter]
        public BExpansionPanel? ExpansionPanel { get; set; }

        [Parameter]
        public bool Eager { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private bool _isBooted;
        private bool _booting;
        private bool _isActive;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (ExpansionPanel != null)
            {
                if ((_isBooted && _booting == false) || Eager)
                {
                    _isActive = ExpansionPanel.InternalIsActive;
                }
                else if (ExpansionPanel.Booted)
                {
                    _isBooted = true;
                    _booting = true;

                    if (ExpansionPanel.InternalIsActive)
                    {
                        await Task.Delay(16);

                        _booting = false;
                        _isActive = true;
                    }
                }
            }
        }
    }
}
