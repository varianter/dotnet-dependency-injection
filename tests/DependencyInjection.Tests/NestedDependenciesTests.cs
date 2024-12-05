using DependencyInjection.Tests.Shared;

namespace DependencyInjection.Tests;

public class NestedDependenciesTests
{
    [Fact]
    public void AddTransient_WithNestedDependency_ShouldResolveCorrectly()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IService, ServiceA>();
        serviceCollection.AddTransient<IDependentService, DependentService>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var rootService = serviceProvider.GetService<IDependentService>();

        Assert.NotNull(rootService);
        Assert.NotNull(rootService.NestedService);
        Assert.IsType<ServiceA>(rootService.NestedService);
    }

    [Fact]
    public void AddTransient_WithMultipleNestedDependencies_ShouldResolveCorrectly()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IService, ServiceA>();
        serviceCollection.AddTransient<IDependentService, DependentService>();
        serviceCollection.AddTransient<IMultiDependentService, MultiDependentService>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var rootService = serviceProvider.GetService<IMultiDependentService>();

        Assert.NotNull(rootService);
        Assert.NotNull(rootService.NestedServiceA);
        Assert.NotNull(rootService.NestedServiceB);
        Assert.IsType<ServiceA>(rootService.NestedServiceA);
        Assert.IsType<DependentService>(rootService.NestedServiceB);
        Assert.IsType<ServiceA>(rootService.NestedServiceB.NestedService);
    }

    private interface IDependentService
    {
        IService NestedService { get; }
    }

    private class DependentService : IDependentService
    {
        public DependentService(IService service)
        {
            NestedService = service;
        }

        public IService NestedService { get; }
    }

    private interface IMultiDependentService
    {
        IService NestedServiceA { get; }
        IDependentService NestedServiceB { get; }
    }

    private class MultiDependentService : IMultiDependentService
    {
        public MultiDependentService(IService serviceA, IDependentService serviceB)
        {
            NestedServiceA = serviceA;
            NestedServiceB = serviceB;
        }

        public IService NestedServiceA { get; }
        public IDependentService NestedServiceB { get; }
    }
}