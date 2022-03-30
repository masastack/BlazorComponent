using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BMainWrap<TMain> where TMain : IMain
    {
        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
