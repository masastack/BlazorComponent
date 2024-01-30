using Microsoft.AspNetCore.Components.Routing;

namespace BlazorComponent;

public partial class BRoutableGroupItem<TGroup> : BGroupItem<TGroup>, IRoutable
    where TGroup : ItemGroupBase
{
    public BRoutableGroupItem(GroupType groupType, string defaultTag = "div") : base(groupType)
    {
        Tag = defaultTag;
    }

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Parameter]
    public string? Href { get; set; }

    [Parameter]
    public string? Tag { get; set; }

    [Parameter]
    public string? Target { get; set; }

    [Parameter]
    public bool Link { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <inheritdoc />
    [Parameter]
    public bool Exact { get; set; }

    /// <inheritdoc />
    [Parameter]
    public string? MatchPattern { get; set; }

    protected IRoutable? Router { get; private set; }

    protected virtual bool IsRoutable => Href != null && RoutableAncestor?.Routable is true;

    protected override bool IsEager => true;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Router = new Router(this);
        await UpdateActiveForRoutable();
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Router = new Router(this);

        (Tag, Attributes) = Router.GenerateRouteLink();
    }

    private async void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var shouldRender = await UpdateActiveForRoutable();
        if (shouldRender)
        {
            await InvokeStateHasChangedAsync();
        }
    }

    private async Task<bool> UpdateActiveForRoutable()
    {
        if (!IsRoutable || Router is null) return false;

        var isActive = InternalIsActive;

        var matched = Router.MatchRoute();

        await SetInternalIsActive(matched, true);

        if (matched && ItemGroup is not null && !isActive)
        {
            await ItemGroup.ToggleAsync(Value);
            await OnActiveUpdatedForRoutable();
        }

        return isActive != matched;
    }

    protected virtual Task OnActiveUpdatedForRoutable() => Task.CompletedTask;

    protected override ValueTask DisposeAsyncCore()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;

        return base.DisposeAsyncCore();
    }
}
