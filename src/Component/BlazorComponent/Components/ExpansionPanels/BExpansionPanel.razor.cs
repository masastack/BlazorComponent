namespace BlazorComponent
{
    public partial class BExpansionPanel : BGroupItem<BExpansionPanels>
    {
        public BExpansionPanel() : base(GroupType.ExpansionPanels)
        {
        }

        [Parameter]
        public bool Readonly { get; set; }

        public bool NextActive => ItemGroup != null && ItemGroup.NextActiveKeys.Contains(Value);

        public bool IsDisabled => ItemGroup != null && (ItemGroup.Disabled || Disabled);

        public bool IsReadonly => ItemGroup != null && (ItemGroup.Readonly || Readonly);

        public async Task Toggle()
        {
            await ToggleAsync();
        }
    }
}