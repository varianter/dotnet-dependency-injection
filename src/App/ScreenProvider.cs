using App.Screens;
using IServiceProvider = DependencyInjection.IServiceProvider;

namespace App;

public class ScreenProvider(IServiceProvider serviceProvider)
{
    public IScreen[] GetScreens()
    {
        return serviceProvider.GetServices<IScreen>().ToArray();
    }
    
    public IScreen GetScreen(Type screenType)
    {
        return (IScreen) serviceProvider.GetService(screenType);
    }
}