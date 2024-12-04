namespace DependencyInjection;

public class ServiceProvider : IServiceProvider
{
    public T? GetService<T>()
    {
        throw new NotImplementedException();
    }

    public object? GetService(Type serviceType)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetServices<T>()
    {
        throw new NotImplementedException();
    }

    public IServiceScope CreateScope()
    {
        throw new NotImplementedException();
    }
}