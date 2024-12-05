namespace DependencyInjection;

public class ServiceProvider(IEnumerable<ServiceDescriptor> descriptors) : IServiceProvider, IDisposable
{
    private readonly IReadOnlyList<ServiceDescriptor> _descriptors = descriptors.ToList();
    private readonly Dictionary<ServiceDescriptor, object> _scopedInstances = new();
    private bool _disposed;

    public T? GetService<T>()
    {
        return (T?)GetService(typeof(T));
    }

    public object? GetService(Type serviceType)
    {
        return GetService(serviceType, []);
    }

    public IEnumerable<T> GetServices<T>()
    {
        var type = typeof(T);

        var matchingDescriptors = _descriptors
            .Where(sd => sd.ServiceType == type)
            .ToList();

        return matchingDescriptors
            .Select(sd => (T)CreateInstance(sd, []))
            .ToArray();
    }

    public IServiceScope CreateScope()
    {
        return new ServiceScope(_descriptors);
    }

    private object? GetService(Type serviceType, HashSet<ServiceDescriptor> visitedDescriptors)
    {
        var descriptor = _descriptors.FirstOrDefault(d => d.ServiceType == serviceType);

        return descriptor != null ? CreateInstance(descriptor, visitedDescriptors) : default;
    }

    private object? CreateInstance(ServiceDescriptor descriptor, HashSet<ServiceDescriptor> visitedDescriptors)
    {
        if (IsCircularDependency(descriptor, visitedDescriptors))
        {
            throw new InvalidOperationException("Circular dependency detected.");
        }

        if (descriptor.ImplementationInstance != null)
        {
            return descriptor.ImplementationInstance;
        }

        if (_scopedInstances.TryGetValue(descriptor, out var scopedInstance))
        {
            return scopedInstance;
        }

        var constructor = descriptor.ImplementationType.GetConstructors().First();

        var parameters = constructor.GetParameters()
            .Select(p => GetService(p.ParameterType, visitedDescriptors))
            .ToArray();

        var instance = Activator.CreateInstance(descriptor.ImplementationType, parameters);

        if (descriptor.Lifetime == ServiceLifetime.Singleton)
        {
            descriptor.ImplementationInstance = instance;
        }

        if (descriptor.Lifetime == ServiceLifetime.Scoped)
        {
            _scopedInstances[descriptor] = instance;
        }
        
        // Chain of nested dependencies is resolved,
        // so remove the current descriptor from the visited descriptors
        visitedDescriptors.Remove(descriptor);

        return instance;
    }

    private static bool IsCircularDependency(ServiceDescriptor descriptor,
        HashSet<ServiceDescriptor> visitedDescriptors)
    {
        return !visitedDescriptors.Add(descriptor);
    }

    public void Dispose()
    {
        if (_disposed) return;

        foreach (var instance in _scopedInstances.Values)
        {
            if (instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        _disposed = true;
    }
}