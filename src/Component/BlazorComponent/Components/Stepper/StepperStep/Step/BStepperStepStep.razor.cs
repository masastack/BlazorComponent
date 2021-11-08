using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BStepperStepStep<TStepper> where TStepper : IStepperStep
    {
        public bool HasError => Component.HasError;

        public string ErrorIcon => Component.ErrorIcon;

        public string CompleteIcon => Component.CompleteIcon;

        public string EditIcon => Component.EditIcon;

        public bool Complete => Component.Complete;

        public bool Editable => Component.Editable;

        public int Step => Component.Step;
    }
}
