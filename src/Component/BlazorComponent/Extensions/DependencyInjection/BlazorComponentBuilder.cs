namespace Microsoft.Extensions.DependencyInjection;

public class BlazorComponentBuilder : IBlazorComponentBuilder
{
    public BlazorComponentBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; set; }
}
