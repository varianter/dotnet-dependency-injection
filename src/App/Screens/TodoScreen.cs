using Database;

namespace App.Screens;

public class TodoScreen(ITodoRepository todoRepository) : IScreen
{
    public void Render()
    {
        Console.WriteLine("Welcome to the Todo Screen! Choose your action:");
        Console.WriteLine("[a]: Add Todo");
        Console.WriteLine("[c]: Complete Todo");
        Console.WriteLine(string.Empty);

        RenderCurrentTodos();
    }

    public void AcceptKey(char key)
    {
        Console.Clear();
        switch (key)
        {
            case 'a':
                Console.WriteLine("Enter the title of the todo:");
                var title = Console.ReadLine();
                todoRepository.Add(title);
                break;
            case 'c':
                RenderCurrentTodos();
                Console.WriteLine("Enter the id of the todo to complete:");
                var id = int.Parse(Console.ReadLine());
                todoRepository.CompleteTodoById(id);
                break;
        }
    }

    private void RenderCurrentTodos()
    {
        Console.WriteLine("Current todos:");
        var todos = todoRepository.GetAll();
        foreach (var todo in todos)
        {
            Console.WriteLine($"[{todo.Id}] {todo.Title} {(todo.IsComplete ? "✅" : "❌")}");
        }
    }

    public string Name => "Todos";
}