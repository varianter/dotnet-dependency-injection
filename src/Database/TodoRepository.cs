namespace Database;

public class TodoRepository : ITodoRepository
{
    private readonly List<Todo> _todos = [];

    public Todo? GetById(int id)
    {
        return _todos.FirstOrDefault(todo => todo.Id == id);
    }

    public Todo[] GetAll()
    {
        return _todos.ToArray();
    }
    
    public Todo Add(string title)
    {
        var todo = new Todo { Id = _todos.Count + 1, Title = title, IsComplete = false };
        _todos.Add(todo);
        return todo;
    }

    public void CompleteTodoById(int id)
    {
        var todo = GetById(id);
        if (todo != null)
        {
            todo.IsComplete = true;
        }
    }
}