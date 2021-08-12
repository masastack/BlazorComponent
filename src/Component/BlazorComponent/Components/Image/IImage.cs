using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IImage : IAbstractComponent,IResponsive
    {
        public RenderFragment ChildContent  { get;  }
        
        [Parameter] 
        public bool Contain { get; set; }

        [Parameter]
        public string LazySrc { get; set; }
        
        [Parameter] 
        public string Src { get; set; }

        [Parameter] 
        public string Gradient { get; set; }
    }
}