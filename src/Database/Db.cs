namespace Database;

public class Db : IDb
{
    public List<Todo> DbTodos { get; } = new List<Todo>();
}