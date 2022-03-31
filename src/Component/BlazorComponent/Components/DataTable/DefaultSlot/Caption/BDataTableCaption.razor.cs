using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableCaption<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public string Caption => Component.Caption;

        public RenderFragment CaptionContent => Component.CaptionContent;
    }
}
