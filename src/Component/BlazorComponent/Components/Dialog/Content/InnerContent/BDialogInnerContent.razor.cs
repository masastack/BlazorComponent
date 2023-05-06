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

        public RenderFragment? ComponentChildContent => Component.ChildContent;
    }
}
