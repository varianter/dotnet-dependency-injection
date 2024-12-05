namespace Database;

public class TodoRepository(IDb db) : ITodoRepository
{
    public Todo? GetById(int id)
    {
        return db.DbTodos.FirstOrDefault(todo => todo.Id == id);
    }

    public Todo[] GetAll()
    {
        return db.DbTodos.ToArray();
    }
    
    public Todo Add(string title)
    {
        var todo = new Todo { Id = db.DbTodos.Count + 1, Title = title, IsComplete = false };
        db.DbTodos.Add(todo);
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