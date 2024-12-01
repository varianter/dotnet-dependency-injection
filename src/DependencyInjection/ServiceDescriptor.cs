namespace DependencyInjection;

public class ServiceDescriptor
{
    public ServiceLifetime Lifetime { get; }

    public Type ServiceType { get; }

    private Type? ImplementationType;
}