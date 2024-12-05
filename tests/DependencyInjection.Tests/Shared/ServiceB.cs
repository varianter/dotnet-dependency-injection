namespace DependencyInjection.Tests.Shared;

public class ServiceB : IService
{
    public string GetMessage()
    {
        return "Hello from ServiceB";
    }
}