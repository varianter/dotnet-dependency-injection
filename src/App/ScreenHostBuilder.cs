using System.Reflection;
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
    
    public ScreenHostBuilder AddScreen<TScreen>() where TScreen : class, IScreen
    {
        Services.AddScoped<IScreen, TScreen>();
        
        return this;
    }

    public ScreenHost Build()
    {
        var serviceProvider = Services.BuildServiceProvider();
        return new ScreenHost(serviceProvider);
    }
}