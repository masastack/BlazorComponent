using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IIcon:IAbstractComponent
    {
        public bool Dense { get; set; }

        public bool Disabled { get; set; }

        public bool Left { get; set; }

        public bool Right { get; set; }

        public StringNumber Size { get; set; }

        public EventCallback<MouseEventArgs> OnClick { get; set; }


        Task HandleOnClick(MouseEventArgs args);

    }
}
