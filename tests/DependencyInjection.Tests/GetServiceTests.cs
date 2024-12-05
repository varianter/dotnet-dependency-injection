using DependencyInjection.Tests.Shared;

namespace DependencyInjection.Tests;

public class GetServiceTests
{
    [Fact]
    public void GetService_WithTransientServiceAndImplementationType_ShouldResolveByService()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IService, ServiceA>();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var byServiceType = serviceProvider.GetService<IService>();
        Assert.NotNull(byServiceType);
        Assert.IsType<ServiceA>(byServiceType);

        var byImplementationType = serviceProvider.GetService<ServiceA>();
        Assert.Null(byImplementationType);
    }

    [Fact]
    public void GetService_WithTransientServiceType_ShouldResolveByService()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient(typeof(ServiceA));
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var byServiceType = serviceProvider.GetService<ServiceA>();
        Assert.NotNull(byServiceType);
        Assert.IsType<ServiceA>(byServiceType);

        var byInheritedType = serviceProvider.GetService<IService>();
        Assert.Null(byInheritedType);
    }

    [Fact]
    public void GetService_WithScopedServiceAndImplementationType_ShouldResolveByService()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IService, ServiceA>();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var byServiceType = serviceProvider.GetService<IService>();
        Assert.NotNull(byServiceType);
        Assert.IsType<ServiceA>(byServiceType);

        var byImplementationType = serviceProvider.GetService<ServiceA>();
        Assert.Null(byImplementationType);
    }

    [Fact]
    public void GetService_WithScopedServiceType_ShouldResolveByService()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped(typeof(ServiceA));
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var byServiceType = serviceProvider.GetService<ServiceA>();
        Assert.NotNull(byServiceType);
        Assert.IsType<ServiceA>(byServiceType);

        var byInheritedType = serviceProvider.GetService<IService>();
        Assert.Null(byInheritedType);
    }

    [Fact]
    public void GetService_WithSingletonServiceAndImplementationType_ShouldResolveByService()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IService, ServiceA>();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var byServiceType = serviceProvider.GetService<IService>();
        Assert.NotNull(byServiceType);
        Assert.IsType<ServiceA>(byServiceType);

        var byImplementationType = serviceProvider.GetService<ServiceA>();
        Assert.Null(byImplementationType);
    }

    [Fact]
    public void GetService_WithSingletonServiceType_ShouldResolveByService()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(typeof(ServiceA));
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var byServiceType = serviceProvider.GetService<ServiceA>();
        Assert.NotNull(byServiceType);
        Assert.IsType<ServiceA>(byServiceType);

        var byInheritedType = serviceProvider.GetService<IService>();
        Assert.Null(byInheritedType);
    }

    [Fact]
    public void GetServices_WithRegisteredService_ShouldReturnService()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IService, ServiceA>();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var services = serviceProvider.GetServices<IService>();

        Assert.NotNull(services);
        Assert.Single(services);
        Assert.All(services, service => Assert.IsType<ServiceA>(service));
    }

    [Fact]
    public void GetServices_WithSeveralOfSameInheritedType_ShouldReturnServices()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IService, ServiceA>();
        serviceCollection.AddTransient<IService, ServiceB>();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var services = serviceProvider.GetServices<IService>();

        Assert.NotNull(services);
        Assert.Equal(2, services.Count());
        Assert.All(services, service => Assert.IsAssignableFrom<IService>(service));
    }

    [Fact]
    public void GetServices_WithDifferentTypesRegistered_ShouldOnlyReturnRequestedType()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IService, ServiceA>();
        serviceCollection.AddTransient<IService, ServiceB>();
        serviceCollection.AddTransient<IAnotherService, AnotherService>();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var services = serviceProvider.GetServices<IService>();

        Assert.NotNull(services);
        Assert.Equal(2, services.Count());
        Assert.All(services, service => Assert.IsAssignableFrom<IService>(service));
    }

    private interface IAnotherService
    {

    }

    public class AnotherService : IAnotherService
    {

    }
}