namespace ToDos.Application.CreateToDos;

public record ToDo(string Title, string Description, bool IsCompleted)
{
    public int Id { get; private set; }

    public void SetId(int id)
    {
        Id = id;
    }
}