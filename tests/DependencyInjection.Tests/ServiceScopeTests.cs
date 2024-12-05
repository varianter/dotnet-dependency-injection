namespace DependencyInjection.Tests;

public class ServiceScopeTests
{
    private interface IServiceA
    {
        public string Id { get; }
    }

    private class ServiceA : IServiceA
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }

    [Fact]
    public void AddScoped_ResolvedInSameScope_ShouldBeSame()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IServiceA, ServiceA>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        IServiceA service1;
        IServiceA service2;
        using (var scope = serviceProvider.CreateScope())
        {
            service1 = scope.ServiceProvider.GetService<IServiceA>();
            service2 = scope.ServiceProvider.GetService<IServiceA>();
            Assert.NotNull(service1);
            Assert.NotNull(service2);

            Assert.Same(service1, service2);
        }

        using (var scope = serviceProvider.CreateScope())
        {
            var service3 = scope.ServiceProvider.GetService<IServiceA>();
            Assert.NotNull(service3);

            Assert.NotSame(service1, service3);
            Assert.NotSame(service2, service3);
        }
    }

    [Fact]
    public void AddTransient_Resolved_ShouldAlwaysBeDifferent()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IServiceA, ServiceA>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var service1 = serviceProvider.GetService<IServiceA>();
        var service2 = serviceProvider.GetService<IServiceA>();

        Assert.NotNull(service1);
        Assert.NotNull(service2);

        Assert.NotSame(service1, service2);
    }

    [Fact]
    public void AddSingleton_Resolved_ShouldBeSameAcrossScopes()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IServiceA, ServiceA>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        IServiceA service1;
        using (var scope = serviceProvider.CreateScope())
        {
            service1 = scope.ServiceProvider.GetService<IServiceA>();
            Assert.NotNull(service1);
        }

        IServiceA service2;
        using (var scope = serviceProvider.CreateScope())
        {
            service2 = scope.ServiceProvider.GetService<IServiceA>();
            Assert.NotNull(service2);
        }

        Assert.Same(service1, service2);
    }
}