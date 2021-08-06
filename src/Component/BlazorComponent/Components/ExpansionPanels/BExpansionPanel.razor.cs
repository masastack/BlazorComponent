using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanel : BGroupItem<BExpansionPanels>
    {
        private bool _disabled;

        public BExpansionPanel() : base(GroupType.ExpansionPanels)
        {
        }

        public bool Expanded => ItemGroup.Values.Contains(Value);

        public bool NextActive => ItemGroup.NextActiveKeys.Contains(Value);

        [Parameter]
        public bool Disabled
        {
            get => ItemGroup.Disabled || _disabled;
            set => _disabled = value;
        }

        public async Task Toggle()
        {
            await ToggleItem();
        }
    }
}
