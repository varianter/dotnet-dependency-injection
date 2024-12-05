using DependencyInjection.Tests.Shared;

namespace DependencyInjection.Tests;

public class GetTests
{
    [Fact]
    public void GetService_WithRegisteredService_ShouldReturnService()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IService, ServiceA>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var service = serviceProvider.GetService<IService>();
        
        Assert.NotNull(service);
        Assert.IsType<ServiceA>(service);
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