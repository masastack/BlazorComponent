using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BStepperStepLabel<TStepper> where TStepper : IStepperStep
    {
        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
