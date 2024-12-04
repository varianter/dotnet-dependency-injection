using App.Screens;

namespace App;

public class ScreenHost(ScreenProvider screenProvider)
{
    // Screen "routes" - the screen names and their corresponding types
    // Trying to model a simple routing system like in web applications
    private readonly Dictionary<string, Type> _routes = new();

    private IScreen _activeScreen;
    
    public void Run()
    {
        MapScreenRoutes();
        SetActiveRouteByIndex(0);
        
        RenderMenu();
        RenderActiveScreen();

        while (true)
        {
            var key = Console.ReadKey(true);
            
            if (key.Key == ConsoleKey.X)
            {
                break;
            }

            if (IsScreenSwitch(key.KeyChar, out var newIndex))
            {
                SetActiveRouteByIndex(newIndex);
            }
            else
            {
                SendKeyToActiveScreen(key.KeyChar);
            }

            Console.Clear();

            RenderMenu();
            RenderActiveScreen();
        }
        
        Console.WriteLine("You pressed 'x' to exit, goodbye!");
    }
    
    private void SendKeyToActiveScreen(char key)
    {
        _activeScreen.AcceptKey(key);
    }

    private bool IsScreenSwitch(char key, out int index)
    {
        if (int.TryParse(key.ToString(), out var number) && number < _routes.Count)
        {
            index = number;
            return true;
        }

        index = -1;
        return false;
    }

    private void RenderMenu()
    {
        Console.WriteLine("----Menu---");
        var screenNames = _routes.Keys.ToArray();
        for (var i = 0; i < screenNames.Length; i++)
        {
            Console.WriteLine($"[{i}]: {screenNames[i]}");;
        }
        Console.WriteLine("[x]: Exit");

        Console.WriteLine("-----------");
        Console.WriteLine(string.Empty);
    }

    private void RenderActiveScreen()
    {
        Console.WriteLine($"---{_activeScreen.Name}---");
        _activeScreen.Render();
        Console.WriteLine("-----------");
        Console.Write("Choose your action by pressing the corresponding key: ");
    }
    
    private void SetActiveRouteByIndex(int index)
    {
        var screenNames = _routes.Keys.ToArray();
        var newRoute = screenNames[index];
        var screenType = GetScreenType(newRoute);
        _activeScreen = screenProvider.GetScreen(screenType);
    }

    private void MapScreenRoutes()
    {
        var screens = screenProvider.GetScreens();
        foreach (var screen in screens)
        {
            MapRoute(screen.Name, screen.GetType());
        }
    }
    
    private void MapRoute(string route, Type screenType)
    {
        if (!typeof(IScreen).IsAssignableFrom(screenType))
        {
            throw new InvalidOperationException($"{screenType.Name} must implement IScreen.");
        }

        _routes[route] = screenType;
    }

    private Type? GetScreenType(string route)
    {
        _routes.TryGetValue(route, out var screenType);
        return screenType;
    }
}