namespace BlazorComponent;

public interface IRadio<TValue>
{
    TValue? Value { get; }

    void RefreshState();
}
