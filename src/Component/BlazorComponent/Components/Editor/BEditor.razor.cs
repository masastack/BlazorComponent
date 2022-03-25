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
        public virtual string Value { get; set; }
        [Parameter]
        public RenderFragment EditorContent { get; set; }

        [Parameter]
        public RenderFragment ToolBarContent { get; set; }
        [Parameter]
        public string Placeholder { get; set; }
        protected ElementReference EditorElement { get; set; }
        protected ElementReference? ToolBar { get; set; }

        [Parameter]
        public string ToolBarClass { get; set; }
        [Parameter]
        public string ToolBarStyle { get; set; }
        [Parameter]
        public string ElementClass { get; set; }
        [Parameter]
        public string ElementStyle { get; set; }

        protected override Task OnInitializedAsync()
        {
           return base.OnInitializedAsync(); 
        }
        public virtual Task<string> GetContentAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<string> GetHtmlAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<string> GetTextAsync()
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
