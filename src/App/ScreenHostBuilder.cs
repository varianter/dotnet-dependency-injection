using App.Screens;
using Database;
using DependencyInjection;

namespace App;

public class ScreenHostBuilder
{
    public IServiceCollection Services { get; } = new ServiceCollection();
    
    public static ScreenHostBuilder CreateDefaultBuilder()
    {
        var builder = new ScreenHostBuilder();
        
        return builder;
    }
    
    public ScreenHostBuilder AddScreens()
    {
        var screenTypes = typeof(IScreen).Assembly.GetTypes()
            .Where(t => typeof(IScreen).IsAssignableFrom(t) && !t.IsInterface)
            .ToList();
        
        foreach (var screenType in screenTypes)
        {
            Services.AddTransient(screenType);
            Services.AddTransient(typeof(IScreen), screenType);
        }
        
        return this;
    }
    
    public ScreenHost Build()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var screenProvider = new ScreenProvider(serviceProvider);
        return new ScreenHost(screenProvider);
    }
}