using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BChipContent<TChip> where TChip : IChip
    {
        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
