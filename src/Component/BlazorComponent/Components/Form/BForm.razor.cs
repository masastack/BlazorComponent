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
        private object _oldModel;

        [Parameter]
        public RenderFragment<EditContext> ChildContent { get; set; }

        [Parameter]
        public EventCallback<EventArgs> OnSubmit { get; set; }

        [Parameter]
        public object Model { get; set; }

        [Parameter]
        public bool EnableDataAnnotationsValidation { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        public EditContext EditContext { get; protected set; }

        [Parameter]
        public bool Readonly { get; set; }

        protected override void OnParametersSet()
        {
            if (_oldModel != Model)
            {
                EditContext = new EditContext(Model);
                if (EnableDataAnnotationsValidation)
                {
                    EditContext.EnableDataAnnotationsValidation();
                }

                _oldModel = Model;
            }
        }
    }
}
