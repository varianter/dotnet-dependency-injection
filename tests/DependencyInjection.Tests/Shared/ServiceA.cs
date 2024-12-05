namespace DependencyInjection.Tests.Shared;

public class ServiceA : IService
{
    public string GetMessage()
    {
        return "Hello from ServiceA";
    }
}