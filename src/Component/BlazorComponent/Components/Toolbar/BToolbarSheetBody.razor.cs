using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BToolbarSheetBody
    {
        [Parameter]
        public string Src { get; set; }

        [Parameter]
        public RenderFragment<ImgProps> ImgContent { get; set; }

        [Parameter]
        public ComponentCssProvider CssProvider { get; set; }

        [Parameter]
        public ComponentAbstractProvider AbstractProvider { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public bool IsExtended { get; set; }

        [Parameter]
        public RenderFragment ExtensionContent { get; set; }

        public ImgProps ImgProps => new()
        {
            Height = Height,
            Src = Src
        };
    }
}
