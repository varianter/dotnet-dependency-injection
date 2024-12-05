using DependencyInjection.Tests.Shared;

namespace DependencyInjection.Tests;

public class AddServiceTests
{
    [Fact]
    public void AddTransient_WithServiceAndImplementationType_ShouldHaveTransientLifetime()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IService, ServiceA>();
        Assert.Single(serviceCollection.Descriptors);
        var descriptor = serviceCollection.Descriptors.First();
        Assert.Equal(ServiceLifetime.Transient, descriptor.Lifetime);
        Assert.Equal(typeof(IService), descriptor.ServiceType);
        Assert.Equal(typeof(ServiceA), descriptor.ImplementationType);
    }

    [Fact]
    public void AddTransient_WithJustServiceType_ShouldHaveTransientLifetime()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient(typeof(ServiceA));
        Assert.Single(serviceCollection.Descriptors);
        var descriptor = serviceCollection.Descriptors.First();
        Assert.Equal(ServiceLifetime.Transient, descriptor.Lifetime);
        Assert.Equal(typeof(ServiceA), descriptor.ServiceType);
        Assert.Equal(typeof(ServiceA), descriptor.ImplementationType);
    }

    [Fact]
    public void AddScoped_WithServiceAndImplementationType_ShouldHaveScopedLifetime()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IService, ServiceA>();
        Assert.Single(serviceCollection.Descriptors);
        var descriptor = serviceCollection.Descriptors.First();
        Assert.Equal(ServiceLifetime.Scoped, descriptor.Lifetime);
        Assert.Equal(typeof(IService), descriptor.ServiceType);
        Assert.Equal(typeof(ServiceA), descriptor.ImplementationType);
    }
    
    [Fact]
    public void AddScoped_WithJustServiceType_ShouldHaveScopedLifetime()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped(typeof(ServiceA));
        Assert.Single(serviceCollection.Descriptors);
        var descriptor = serviceCollection.Descriptors.First();
        Assert.Equal(ServiceLifetime.Scoped, descriptor.Lifetime);
        Assert.Equal(typeof(ServiceA), descriptor.ServiceType);
        Assert.Equal(typeof(ServiceA), descriptor.ImplementationType);
    }
    
    [Fact]
    public void AddSingleton_WithServiceAndImplementationType_ShouldHaveSingletonLifetime()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IService, ServiceA>();
        Assert.Single(serviceCollection.Descriptors);
        var descriptor = serviceCollection.Descriptors.First();
        Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
        Assert.Equal(typeof(IService), descriptor.ServiceType);
        Assert.Equal(typeof(ServiceA), descriptor.ImplementationType);
    }
    
    [Fact]
    public void AddSingleton_WithJustServiceType_ShouldHaveSingletonLifetime()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(typeof(ServiceA));
        Assert.Single(serviceCollection.Descriptors);
        var descriptor = serviceCollection.Descriptors.First();
        Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
        Assert.Equal(typeof(ServiceA), descriptor.ServiceType);
        Assert.Equal(typeof(ServiceA), descriptor.ImplementationType);
    }
}