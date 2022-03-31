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
