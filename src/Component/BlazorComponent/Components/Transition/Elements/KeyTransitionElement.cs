using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent;

public class KeyTransitionElement<TValue> : TransitionElementBase<TValue>
{
    private TValue? _prevValue;
    private List<KeyTransitionState<TValue>?> _states = new();
    private bool _needARender;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _prevValue = Value;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!EqualityComparer<TValue>.Default.Equals(Value, _prevValue))
        {
            _states.Clear();
            _states.Add(new KeyTransitionState<TValue>(_prevValue, true));
            _states.Add(new KeyTransitionState<TValue>(Value, false));

            _prevValue = Value;

            _needARender = true;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_needARender)
        {
            _needARender = false;

            _states[0].Value = false;
            _states[1].Value = true;

            await Task.Delay(17);

            StateHasChanged();
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (_states.Count == 0)
        {
            BuildRenderTree2(builder, new KeyTransitionState<TValue>(Value, true));
        }
        else
        {
            foreach (var state in _states)
            {
                BuildRenderTree2(builder, state);
            }
        }
    }

    private void BuildRenderTree2(RenderTreeBuilder builder, KeyTransitionState<TValue> state)
    {
        Console.Out.WriteLine($"state.Key ={state.Key}, state.Value = {state.Value}");

        builder.OpenComponent<IfTransitionElement>(0);
        builder.AddAttribute(1, nameof(IfTransitionElement.Value), state.Value);
        builder.AddAttribute(2, nameof(ChildContent), RenderChildContent(state));
        builder.AddAttribute(3, nameof(Tag), Tag);
        builder.SetKey(state.Key);
        builder.CloseComponent();
    }

    private RenderFragment RenderChildContent(KeyTransitionState<TValue> state)
    {
        return builder =>
        {
            builder.OpenComponent<ShouldRenderContainer>(0);
            builder.AddAttribute(1, nameof(ShouldRenderContainer.Value),
                EqualityComparer<TValue>.Default.Equals(state.Key, Value));
            builder.AddAttribute(2, nameof(ChildContent), ChildContent);
            builder.SetKey(state.Key);
            builder.CloseComponent();
        };
    }
}

public class KeyTransitionState<TKey>
{
    public TKey? Key { get; set; }

    public bool Value { get; set; }

    public KeyTransitionState(TKey? key, bool value)
    {
        Key = key;
        Value = value;
    }
}