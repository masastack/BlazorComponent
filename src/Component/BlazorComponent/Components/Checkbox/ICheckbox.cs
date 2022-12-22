namespace BlazorComponent
{
    public interface ICheckbox<TValue> : ISelectable<TValue>
    {
        string ComputedIcon { get; }
    }
}
