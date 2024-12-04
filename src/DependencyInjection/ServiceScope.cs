namespace DependencyInjection;

public class ServiceScope : IServiceScope
{
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public IServiceProvider ServiceProvider { get; }
}