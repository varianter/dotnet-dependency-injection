namespace DependencyInjection;

public class ServiceProvider(IEnumerable<ServiceDescriptor> descriptors) : IServiceProvider
{
    private readonly IReadOnlyList<ServiceDescriptor> _descriptors = descriptors.ToList();

    public T? GetService<T>()
    {
        return (T?)GetService(typeof(T));
    }

    public IEnumerable<T> GetServices<T>()
    {
        return _descriptors
            .Where(d => d.ServiceType == typeof(T))
            .Select(CreateInstance<T>)
            .ToArray();
    }

    public IServiceScope CreateScope()
    {
        return new ServiceScope(_descriptors);
    }
    
    private object GetService(Type serviceType)
    {
        var descriptor = _descriptors.FirstOrDefault(d => d.ServiceType == serviceType);

        return descriptor != null ? CreateInstance(descriptor) : default;
    }
    
    private T? CreateInstance<T>(ServiceDescriptor descriptor)
    {
        return (T?)CreateInstance(descriptor);
    }
    
    private object? CreateInstance(ServiceDescriptor descriptor)
    {
        if (descriptor.ImplementationInstance != null)
        {
            return descriptor.ImplementationInstance;
        }
        
        var constructor = descriptor.ImplementationType.GetConstructors().First();
        
        var parameters = constructor.GetParameters()
            .Select(p => GetService(p.ParameterType))
            .ToArray();

        var instance = Activator.CreateInstance(descriptor.ImplementationType, parameters);
        
        if (descriptor.Lifetime == ServiceLifetime.Singleton)
        {
            descriptor.ImplementationInstance = instance;
        }
        
        return instance;
    }
}