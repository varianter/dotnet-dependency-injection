namespace DependencyInjection;

public class ServiceScope(IEnumerable<ServiceDescriptor> descriptors) : IServiceScope
{
    private readonly ServiceProvider _serviceProvider = new(descriptors);

    public IServiceProvider ServiceProvider => _serviceProvider;

    public void Dispose() => _serviceProvider.Dispose();
}
