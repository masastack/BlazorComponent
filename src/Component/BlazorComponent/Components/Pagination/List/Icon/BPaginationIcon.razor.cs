using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BPaginationIcon<TPagination> where TPagination : IPagination
    {
        [Parameter]
        public int ItemIndex { get; set; }

        public string GetIcon => Component.GetIcon(ItemIndex);

        public bool Disabled => ItemIndex == (int)PaginationIconTypes.First ? Component.PrevDisabled : Component.NextDisabled;

        public string IconClassName() => $"navigation{(Disabled ? "-disabled" : string.Empty)}";

        public string PrevIcon => Component.PrevIcon;

        public async Task HandleAsync(MouseEventArgs args)
        {
            if (GetIcon == PrevIcon)
                await Component.HandlePreviousAsync(args);
            else
                await Component.HandleNextAsync(args);
        }
    }
}
