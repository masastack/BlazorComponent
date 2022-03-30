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
