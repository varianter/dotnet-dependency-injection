namespace DependencyInjection.Tests;

public class ScopedDisposalTests
{
    private interface IMyDisposableService : IDisposable
    {
        bool IsDisposed { get; }
    }

    private class MyDisposableService : IMyDisposableService
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }

    [Fact]
    public void ScopedServices_ShouldBeDisposed_WhenScopeIsDisposed()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IMyDisposableService, MyDisposableService>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        IMyDisposableService? disposableService;

        using (var scope = serviceProvider.CreateScope())
        {
            disposableService = scope.ServiceProvider.GetService<IMyDisposableService>();
            Assert.NotNull(disposableService);
            Assert.False(disposableService.IsDisposed, "Service should not be disposed yet.");
        }

        Assert.True(disposableService.IsDisposed, "Service should be disposed after the scope is disposed.");
    }
}