namespace BlazorComponent
{
    public partial class BStepperStep : BDomComponentBase
    {
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected virtual Task HandleOnClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
