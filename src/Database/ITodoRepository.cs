namespace Database;

public interface ITodoRepository
{
    public Todo? GetById(int id);
    public Todo[] GetAll();
    public Todo Add(string title);
    public void CompleteTodoById(int id);
}