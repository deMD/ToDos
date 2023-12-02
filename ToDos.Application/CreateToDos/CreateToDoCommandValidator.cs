using FluentValidation;

namespace ToDos.Application.CreateToDos;

public class CreateToDoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    public CreateToDoCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}