using App.Screens;

namespace App;

public class ScreenHost(ScreenProvider screenProvider)
{
    // Building screen "routes"
    private readonly string[] _screenNames = screenProvider.GetScreens().Select(screen => screen.Name).ToArray();

    private IScreen _activeScreen;
    
    public void Run()
    {
        _activeScreen = screenProvider.GetScreenByName(_screenNames[0]);
        
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
                _activeScreen = screenProvider.GetScreenByName(_screenNames[newIndex]);
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
        if (int.TryParse(key.ToString(), out var number) && number < _screenNames.Length)
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
        for (var i = 0; i < _screenNames.Length; i++)
        {
            Console.WriteLine($"[{i}]: {_screenNames[i]}");;
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
}