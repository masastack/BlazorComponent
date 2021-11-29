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

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        public EditContext EditContext { get; protected set; }

        protected List<IValidatable> Validatables { get; } = new List<IValidatable>();

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

        public void Register(IValidatable validatable)
        {
            Validatables.Add(validatable);
        }

        public async Task ValidateAsync()
        {
            //REVIEW: We should combine this
            var valid = true;
            if (EditContext != null)
            {
                valid = EditContext.Validate();
            }
            else
            {
                foreach (var validatable in Validatables)
                {
                    var success = await validatable.ValidateAsync();
                    if (!success)
                    {
                        valid = false;
                    }
                }
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(valid);
            }
        }

        public async Task ResetAsync()
        {
            if (EditContext != null)
            {
                EditContext.MarkAsUnmodified();
            }

            foreach (var validatable in Validatables)
            {
                await validatable.ResetAsync();
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(true);
            }
        }

        public async Task ResetValidationAsync()
        {
            if (EditContext != null)
            {
                EditContext.MarkAsUnmodified();
            }

            foreach (var validatable in Validatables)
            {
                await validatable.ResetValidationAsync();
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(true);
            }
        }
    }
}
