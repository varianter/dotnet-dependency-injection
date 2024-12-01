namespace DependencyInjection;

public interface IServiceScope : IDisposable
{
    IServiceProvider ServiceProvider { get; }
}