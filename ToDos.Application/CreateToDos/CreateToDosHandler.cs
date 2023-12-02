using FluentValidation;
using LanguageExt.Common;

namespace ToDos.Application.CreateToDos;

public class CreateToDosHandler
{
    public async Task<Result<ToDoDto>> HandleAsync(CreateToDoCommand toDo)
    {
        var validator = new CreateToDoCommandValidator();
        var validationResult = await validator.ValidateAsync(toDo);
        return !validationResult.IsValid 
            ? new Result<ToDoDto>(new ValidationException("Todo could not be validated.", validationResult.Errors)) 
            : new ToDoDto(toDo.Title, toDo.Description, toDo.IsCompleted);
    }
}