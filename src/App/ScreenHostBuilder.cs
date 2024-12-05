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
    
    public ScreenHostBuilder AddScreens()
    {
        // Hmm, this might become a bit unwieldy if we add more screens and their dependencies
        // Is there some sort of pattern we could use to make this more manageable?:
        var database = new Db();
        var todoRepository = new TodoRepository(database);
        
        _screens.Add(new AboutScreen());
        _screens.Add(new TodoScreen(todoRepository));
        
        return this;
    }
    
    public ScreenHost Build()
    {
        return new ScreenHost(new ScreenProvider(_screens.ToArray()));
    }
}