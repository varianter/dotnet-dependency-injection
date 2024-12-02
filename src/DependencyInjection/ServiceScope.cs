namespace DependencyInjection;

public class ServiceScope(IEnumerable<ServiceDescriptor> descriptors) : IServiceScope
{
    public IServiceProvider ServiceProvider { get; } = new ServiceProvider(descriptors);

    public void Dispose()
    {
        // TODO release managed resources here
    }
}