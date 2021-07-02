using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent
{
    public partial class BForm : BDomComponentBase
    {
        [Parameter]
        public RenderFragment<EditContext> ChildContent { get; set; }

        [Parameter]
        public EventCallback<EventArgs> OnSubmit { get; set; }

        [Parameter]
        public object Model { get; set; }

        [Parameter]
        public bool EnableDataAnnotationsValidation { get; set; }

        protected EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            if (EditContext == null && Model != null)
            {
                EditContext = new EditContext(Model);
                if (EnableDataAnnotationsValidation)
                {
                    EditContext.EnableDataAnnotationsValidation();
                }
            }
        }
    }
}
