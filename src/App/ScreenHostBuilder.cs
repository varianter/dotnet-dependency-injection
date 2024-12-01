using App.Screens;
using Database;

namespace App;

public class ScreenHostBuilder
{
    private readonly List<IScreen> _screens = [];
    
    public static ScreenHostBuilder CreateDefaultBuilder()
    {
        var builder = new ScreenHostBuilder();
        
        // Hmm, this might become a bit unwieldy if we add more screens and their dependencies
        // Is there some sort of pattern we could use to make this more manageable?:
        builder._screens.Add(new AboutScreen());
        builder._screens.Add(new TodoScreen(new TodoRepository()));
        
        return builder;
    }
    
    public ScreenHost Build()
    {
        return new ScreenHost(_screens.ToArray());
    }
}