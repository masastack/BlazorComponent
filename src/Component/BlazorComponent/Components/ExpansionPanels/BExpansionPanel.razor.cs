using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanel : BGroupItem<BExpansionPanels>
    {
        public BExpansionPanel() : base(GroupType.ExpansionPanels)
        {
        }

        [Parameter]
        public bool Readonly { get; set; }

        public bool Expanded => ItemGroup.Values.Contains(Value);

        public bool NextActive => ItemGroup.NextActiveKeys.Contains(Value);

        public bool IsDisabled => ItemGroup.Disabled || Disabled;

        public bool IsReadonly => ItemGroup.Readonly || Readonly;

        public async Task Toggle()
        {
            await ToggleItem();
        }
    }
}