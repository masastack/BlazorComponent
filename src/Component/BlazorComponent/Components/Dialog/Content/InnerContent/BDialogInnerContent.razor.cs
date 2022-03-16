using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDialogInnerContent<TDialog> where TDialog : IDialog
    {
        public ElementReference DialogRef
        {
            set { Component.DialogRef = value; }
        }

        public bool IsActive => Component.IsActive;

        public string Transition => Component.Transition;

        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
