using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BEditor : BDomComponentBase, IEditor
    {
        [Parameter]
        public string? ContentClass { get; set; }

        [Parameter]
        public string? ContentStyle { get; set; }

        [Parameter]
        public string? Placeholder { get; set; }

        [Parameter]
        public RenderFragment? ToolbarContent { get; set; }

        [Parameter]
        public string? ToolbarClass { get; set; }

        [Parameter]
        public string? ToolbarStyle { get; set; }

        [Parameter]
        public virtual string? Value { get; set; }

        public ElementReference ContentRef { get; set; }
        public ElementReference ToolbarRef { get; set; }

        public virtual Task<string?> GetContentAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<string?> GetHtmlAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<string?> GetTextAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task SetContentAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task SetHtmlAsync()
        {
            throw new NotImplementedException();
        }
    }
}
