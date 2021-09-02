using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public interface IAlert : IHasProviderComponent
    {
        #region protected fields

        RenderFragment IconContent { get; }

        bool IsShowIcon { get; }

        #endregion

        Borders Border { get; }

        RenderFragment ChildContent { get; }

        string CloseIcon { get; }

        string CloseLabel { get; }

        string Color { get; }

        bool Dismissible { get; }
        
        string Tag { get; }
        
        AlertTypes Type { get; }
        
        bool Value { get; }
        
        EventCallback<bool> ValueChanged { get; }

        Task HandleOnDismiss(MouseEventArgs args);
    }
}