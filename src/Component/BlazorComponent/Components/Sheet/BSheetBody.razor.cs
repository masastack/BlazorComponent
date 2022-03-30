using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSheetBody<TSheet> where TSheet : ISheet
    {
        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
