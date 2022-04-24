namespace BlazorComponent;

public class BToggleable : BDelayable
{
    [Parameter]
    public bool Value
    {
        get => _value;
        set
        {
            var tempValue = _value;
            _value = value;

            if (tempValue != _value)
            {
                InternalOnValueChanged(value);
            }
        }
    }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    private bool _preventOnValueChanged;
    private bool _value;

    private async void InternalOnValueChanged(bool val)
    {
        if (_preventOnValueChanged)
        {
            _preventOnValueChanged = false;
            return;
        }

        await OnValueChanged(val);
    }

    protected virtual Task OnValueChanged(bool val)
    {
        return Task.CompletedTask;
    }

    protected override async Task WhenIsActiveUpdating(bool val)
    {
        if (ValueChanged.HasDelegate && val != Value)
        {
            _preventOnValueChanged = true;
            await ValueChanged.InvokeAsync(val);
        }
    }
}