
namespace App.Screens;

public interface IScreen
{
    public void Render();
    public void AcceptKey(char key);
    public string Name { get; }
}