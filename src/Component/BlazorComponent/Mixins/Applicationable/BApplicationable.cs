using BlazorComponent.Applicationable;
using BlazorComponent.Services;

namespace BlazorComponent;

public class BApplicationable : BDomComponentBase
{
    [Parameter]
    public bool App
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    public BApplicationable(params string[] parameters)
    {
        _parameters = parameters;
    }

    private readonly string[] _parameters;
    private Services.IApplication _application;

    protected override void OnWatcherInitialized()
    {
        base.OnWatcherInitialized();

        Watcher.Watch<bool>(nameof(App), AppWatcher);

        _parameters.ForEach(parameter => Watcher.Watch(parameter, () => _ = CallUpdate()));
    }

    protected void SetApplication(IApplication application) => _application = application;

    protected virtual TargetProp ApplicationProperty => TargetProp.Unset;

    protected virtual Task<double> UpdateApplication() => Task.FromResult(0d);

    private async void AppWatcher(bool _, bool prev)
    {
        if (!prev)
        {
            await CallUpdate();
        }
    }

    private async Task CallUpdate()
    {
        if (!App) return;

        if (ApplicationProperty == TargetProp.Unset) return;

        var value = await UpdateApplication();

        _application?.Update(ApplicationProperty, value);
    }
}
