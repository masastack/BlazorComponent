namespace BlazorComponent
{
    public partial class BRippleableRipple<TComponent> where TComponent : IRippleable
    {
        public bool Ripple => Component.Ripple ?? true;
    }
}
