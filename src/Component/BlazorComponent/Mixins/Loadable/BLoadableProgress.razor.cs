using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BLoadableProgress<TComponent> where TComponent : ILoadable
    {
        public StringBoolean Loading => Component.Loading;

        public RenderFragment ProgressContent => Component.ProgressContent;
    }
}
