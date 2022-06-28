namespace Microsoft.Extensions.DependencyInjection;

public interface IBlazorComponentBuilder
{
    IServiceCollection Services { get; set; }
}
