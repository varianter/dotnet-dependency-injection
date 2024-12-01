namespace DependencyInjection;

public class ServiceCollection : IServiceCollection
{
    public IServiceCollection AddTransient(Type serviceType, Type implementationType)
    {
        throw new NotImplementedException();
    }

    public IServiceCollection AddTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        throw new NotImplementedException();
    }

    public IServiceCollection AddScoped(Type serviceType, Type implementationType)
    {
        throw new NotImplementedException();
    }

    public IServiceCollection AddScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        throw new NotImplementedException();
    }

    public IServiceCollection AddSingleton(Type serviceType, Type implementationType)
    {
        throw new NotImplementedException();
    }

    public IServiceCollection AddSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        throw new NotImplementedException();
    }

    public IServiceProvider BuildServiceProvider()
    {
        throw new NotImplementedException();
    }
}