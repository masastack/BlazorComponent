using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BStepperStep : BDomComponentBase
    {
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }


        [Parameter]
        public List<Func<bool>> Rules { get; set; } = new();

        public bool HasError => Rules.Any(validate => !validate());

        [Parameter]
        public string ErrorIcon { get; set; } = "mdi-alert";

        [Parameter]
        public string CompleteIcon { get; set; } = "mdi-check";

        [Parameter]
        public string EditIcon { get; set; } = "mdi-pencil";

        [Parameter]
        public bool Editable { get; set; }

        [Parameter]
        public bool Complete { get; set; }

        [Parameter]
        public StringNumber Step { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
