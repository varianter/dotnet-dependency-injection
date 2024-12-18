namespace DependencyInjection;

public class ServiceCollection : IServiceCollection
{
    private readonly List<ServiceDescriptor> _descriptors = [];
    public IEnumerable<ServiceDescriptor> Descriptors => _descriptors;
    
    private IServiceCollection Add(Type serviceType, Type implementationType, ServiceLifetime lifetime)
    {
        throw new NotImplementedException();
    }

    public IServiceProvider BuildServiceProvider()
    {
        throw new NotImplementedException();
    }

    public IServiceCollection AddTransient(Type serviceType, Type implementationType)
    {
        return Add(serviceType, implementationType, ServiceLifetime.Transient);
    }

    public IServiceCollection AddTransient(Type serviceType)
    {
        return AddTransient(serviceType, serviceType);
    }

    public IServiceCollection AddTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        return AddTransient(typeof(TService), typeof(TImplementation));
    }

    public IServiceCollection AddScoped(Type serviceType, Type implementationType)
    {
        return Add(serviceType, implementationType, ServiceLifetime.Scoped);
    }

    public IServiceCollection AddScoped(Type serviceType)
    {
        return AddScoped(serviceType, serviceType);
    }
    
    public IServiceCollection AddScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        return AddScoped(typeof(TService), typeof(TImplementation));
    }

    public IServiceCollection AddSingleton(Type serviceType, Type implementationType)
    {
        return Add(serviceType, implementationType, ServiceLifetime.Singleton);
    }
    
    public IServiceCollection AddSingleton(Type serviceType)
    {
        return AddSingleton(serviceType, serviceType);
    }

    public IServiceCollection AddSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        return AddSingleton(typeof(TService), typeof(TImplementation));
    }
}