using App.Screens;

namespace App;

public class ScreenHost(IScreen[] screens)
{
    private int _activeScreenIndex;
    
    public void Run()
    {
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
                _activeScreenIndex = newIndex;
            }
            else
            {
                SendKeyToActiveScreen(key.KeyChar);
            }

            Console.Clear();

            RenderMenu();
            RenderActiveScreen();
        }
        
        Console.WriteLine("Goodbye!");
    }
    
    private IScreen GetActiveScreen()
    {
        return screens[_activeScreenIndex];
    }

    private void SendKeyToActiveScreen(char key)
    {
        GetActiveScreen().AcceptKey(key);
    }

    private bool IsScreenSwitch(char key, out int index)
    {
        if (int.TryParse(key.ToString(), out var number) && number < screens.Length)
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
        for (var i = 0; i < screens.Length; i++)
        {
            Console.WriteLine($"[{i}]: {screens[i].Name}");
        }
        Console.WriteLine("[x]: Exit");

        Console.WriteLine("-----------");
        Console.WriteLine(string.Empty);
    }

    private void RenderActiveScreen()
    {
        var activeScreen = GetActiveScreen();
        Console.WriteLine($"---{activeScreen.Name}---");
        activeScreen.Render();
        Console.WriteLine("-----------");
        Console.Write("Choose your action by pressing the corresponding key: ");
    }
}