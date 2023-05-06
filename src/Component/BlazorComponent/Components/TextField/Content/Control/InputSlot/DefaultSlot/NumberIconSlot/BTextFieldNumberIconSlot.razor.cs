namespace BlazorComponent
{
    public partial class BTextFieldNumberIconSlot<TValue, TInput> where TInput : ITextField<TValue>
    {
        public string Type => Component.Type;

        public TextFieldNumberProperty Props => Component.Props;

        public EventCallback<MouseEventArgs> HandleOnNumberUpClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnNumberUpClickAsync);

        public EventCallback<MouseEventArgs> HandleOnNumberDownClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnNumberDownClickAsync);

    }
}
