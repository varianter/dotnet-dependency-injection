namespace DependencyInjection;

public class ServiceDescriptor
{
    public required ServiceLifetime Lifetime { get; init; }

    public required Type ServiceType { get; init; }

    public required Type ImplementationType { get; init; }
    
    internal object ImplementationInstance { get; set; }
}