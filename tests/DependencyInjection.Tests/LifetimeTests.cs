using DependencyInjection.Tests.Shared;

namespace DependencyInjection.Tests;

public class LifetimeTests
{
    [Fact]
    public void AddScoped_ResolvedInSameScope_ShouldBeSame()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IService, ServiceA>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        IService service1;
        IService service2;
        using (var scope = serviceProvider.CreateScope())
        {
            service1 = scope.ServiceProvider.GetService<IService>();
            service2 = scope.ServiceProvider.GetService<IService>();
            Assert.NotNull(service1);
            Assert.NotNull(service2);

            Assert.Same(service1, service2);
        }

        using (var scope = serviceProvider.CreateScope())
        {
            var service3 = scope.ServiceProvider.GetService<IService>();
            Assert.NotNull(service3);

            Assert.NotSame(service1, service3);
            Assert.NotSame(service2, service3);
        }
    }

    [Fact]
    public void AddTransient_Resolved_ShouldAlwaysBeDifferent()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IService, ServiceA>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var service1 = serviceProvider.GetService<IService>();
        var service2 = serviceProvider.GetService<IService>();

        Assert.NotNull(service1);
        Assert.NotNull(service2);

        Assert.NotSame(service1, service2);
    }

    [Fact]
    public void AddSingleton_Resolved_ShouldBeSameAcrossScopes()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IService, ServiceA>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        IService service1;
        using (var scope = serviceProvider.CreateScope())
        {
            service1 = scope.ServiceProvider.GetService<IService>();
            Assert.NotNull(service1);
        }

        IService service2;
        using (var scope = serviceProvider.CreateScope())
        {
            service2 = scope.ServiceProvider.GetService<IService>();
            Assert.NotNull(service2);
        }

        Assert.Same(service1, service2);
    }
}