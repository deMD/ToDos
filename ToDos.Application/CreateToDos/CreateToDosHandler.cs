using FluentValidation;
using LanguageExt.Common;

namespace ToDos.Application.CreateToDos;

public class CreateToDosHandler
{
    private readonly CreateToDoService _createToDoService;

    public CreateToDosHandler(CreateToDoService createToDoService)
    {
        _createToDoService = createToDoService;
    }

    public async Task<Result<ToDoDto>> HandleAsync(CreateToDoCommand command)
    {
        var validator = new CreateToDoCommandValidator();
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return new Result<ToDoDto>(new ValidationException("Todo could not be validated.",
                validationResult.Errors));

        var saveResult = await _createToDoService.SaveToDoAsync(command.ToToDo());
        return saveResult.Match(
            x => x.ToToDoDto(),
            ex => new Result<ToDoDto>(ex));
    }
}