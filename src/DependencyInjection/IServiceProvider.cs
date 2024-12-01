namespace DependencyInjection;

public interface IServiceProvider
{
    public T? GetService<T>();
    public IEnumerable<T> GetServices<T>();
    public IServiceScope CreateScope();
}