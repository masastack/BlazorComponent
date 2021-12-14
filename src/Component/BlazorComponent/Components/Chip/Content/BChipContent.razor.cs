using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BChipContent<TChip> where TChip : IChip
    {
        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
