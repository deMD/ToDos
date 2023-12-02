namespace ToDos.Application.CreateToDos;

public record CreateToDoCommand(string Title, string Description, bool IsCompleted);