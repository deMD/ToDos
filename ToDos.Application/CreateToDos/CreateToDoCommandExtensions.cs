namespace ToDos.Application.CreateToDos;

public static class CreateToDoCommandExtensions
{
    public static ToDo ToToDo(this CreateToDoCommand command)
    {
        return new ToDo(command.Title, command.Description, command.IsCompleted);
    }
}