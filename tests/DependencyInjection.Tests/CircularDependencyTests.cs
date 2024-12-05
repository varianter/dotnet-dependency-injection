namespace DependencyInjection.Tests;

public class CircularDependencyTests
{
    private interface IServiceA
    {
        string GetMessage();
    }

    private interface IServiceB
    {
        string GetMessage();
    }

    private class ServiceA : IServiceA
    {
        private readonly IServiceB _serviceB;
        public ServiceA(IServiceB serviceB)
        {
            _serviceB = serviceB;
        }

        public string GetMessage() => "A depends on B: " + _serviceB.GetMessage();
    }

    private class ServiceB : IServiceB
    {
        private readonly IServiceA _serviceA;
        public ServiceB(IServiceA serviceA)
        {
            _serviceA = serviceA;
        }

        public string GetMessage() => "B depends on A: " + _serviceA.GetMessage();
    }

    [Fact]
    public void ResolvingCircularDependencies_ShouldThrow()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IServiceA, ServiceA>();
        serviceCollection.AddTransient<IServiceB, ServiceB>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        Assert.Throws<InvalidOperationException>(() => serviceProvider.GetService<IServiceA>());
    }
}