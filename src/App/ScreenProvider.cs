using App.Screens;

namespace App;

public class ScreenProvider(IScreen[] screens)
{
    public IScreen[] GetScreens()
    {
        return screens;
    }
    
    public IScreen GetScreen(Type screenType)
    {
        return screens.FirstOrDefault(screen => screen.GetType() == screenType);
    }
}