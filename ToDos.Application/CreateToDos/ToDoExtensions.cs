namespace ToDos.Application.CreateToDos;

public static class ToDoExtensions
{
    public static ToDoDto ToToDoDto(this ToDo toDo)
    {
        return new ToDoDto(toDo.Title, toDo.Description, toDo.IsCompleted);
    }
}