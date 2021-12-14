using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BChipClose<TChip> where TChip : IChip
    {
        public bool Close => Component.Close;

        string CloseIcon => Component.CloseIcon;
    }
}
