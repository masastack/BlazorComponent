using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class StepperStepAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyStepperStepDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BStepperStepStep<>), typeof(BStepperStepStep<IStepperStep>))
                .Apply(typeof(BStepperStepLabel<>), typeof(BStepperStepLabel<IStepperStep>));
        }
    }
}
