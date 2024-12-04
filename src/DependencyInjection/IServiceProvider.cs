namespace DependencyInjection;

public interface IServiceProvider
{
    public T? GetService<T>();
    public object? GetService(Type serviceType);
    public IEnumerable<T> GetServices<T>();
    public IServiceScope CreateScope();
}