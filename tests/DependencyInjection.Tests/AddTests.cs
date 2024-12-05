using DependencyInjection.Tests.Shared;

namespace DependencyInjection.Tests;

public class AddTests
{
    [Fact]
    public void AddTransient_WithServiceAndImplementationType_ShouldResolveByService()
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
    public void AddTransient_WithServiceType_ShouldResolveByService()
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
    public void AddScoped_WithServiceAndImplementationType_ShouldResolveByService()
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
    public void AddScoped_WithServiceType_ShouldResolveByService()
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
    public void AddSingleton_WithServiceAndImplementationType_ShouldResolveByService()
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
    public void AddSingleton_WithServiceType_ShouldResolveByService()
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
}