using App.Screens;
using Database;

namespace App;

public class ScreenHostBuilder
{
    private readonly List<IScreen> _screens = [];
    
    public static ScreenHostBuilder CreateDefaultBuilder()
    {
        var builder = new ScreenHostBuilder();
        
        return builder;
    }
    
    public ScreenHostBuilder AddScreen<TScreen>(TScreen screen) where TScreen : class, IScreen
    {
        _screens.Add(screen);
        
        return this;
    }
    
    public ScreenHost Build()
    {
        return new ScreenHost(new ScreenProvider(_screens.ToArray()));
    }
}