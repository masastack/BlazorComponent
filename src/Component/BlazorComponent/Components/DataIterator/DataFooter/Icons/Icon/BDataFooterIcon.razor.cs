namespace BlazorComponent
{
    public partial class BDataFooterIcon<TComponent> where TComponent : IDataFooter
    {
        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        public bool DisablePagination => Component.DisablePagination;

        public bool IsDisabled => Disabled || DisablePagination;
    }
}
