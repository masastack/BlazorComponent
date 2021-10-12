using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BChipFilter<TChip> where TChip : IChip
    {
        public bool Filter => Component.Filter;

        string FilterIcon => Component.FilterIcon;

        public bool IsActive => Component.IsActive;
    }
}
