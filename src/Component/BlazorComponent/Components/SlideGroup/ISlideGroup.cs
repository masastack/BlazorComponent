using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface ISlideGroup : IHasProviderComponent
    {
        string ActiveClass { get; }

        bool CenterActive { get; }

        bool Mandatory { get; }

        StringNumber Max { get; }

        bool Multiple { get; }

        string NextIcon { get; }
        
        RenderFragment NextContent { get; set; }

        string PrevIcon { get; }
        
        RenderFragment PrevContent { get; set; }

        internal Task OnAffixClick(string direction);

        bool HasNext { get; }

        bool HasPrev { get; }

        StringBoolean ShowArrows { get; }
    }
}