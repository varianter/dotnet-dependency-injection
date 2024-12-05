
namespace App.Screens;

public class AboutScreen : IScreen
{
    private string Buffer = "This is a project to demonstrate how to build your own Dependency Injection container!";
    private string KeyPresses = string.Empty;
    
    public void Render()
    {
        Console.WriteLine(Buffer);
    }

    public void AcceptKey(char key)
    {
        KeyPresses += key;
        Buffer = $"Wow you pressed some keys: {KeyPresses}. How about you build your own DI container instead?";
    }

    public string Name => "About";
}