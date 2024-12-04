using App.Screens;

namespace App;

public class ScreenProvider(IScreen[] screens)
{
    public IScreen[] GetScreens()
    {
        return screens;
    }
    
    public IScreen GetScreenByName(string name)
    {
        return screens.FirstOrDefault(screen => screen.Name == name);
    }
}