using FluentAssertions;
using FluentValidation;
using ToDos.Application.CreateToDos;

namespace ToDos.Application.Tests.ToDos.CreateToDos;

public class CreateToDosHandlerTests
{
    [Fact]
    public async Task ItShouldCreateAToDo()
    {
        var toDo = new CreateToDoCommand("Test", "Test description", false);
        var sut = GetCreateToDosHandler();

        var result = await sut.HandleAsync(toDo);
        result.IsSuccess.Should().BeTrue();
        result.IfSucc(x =>
        {
            x.Title.Should().Be(toDo.Title);
            x.Description.Should().Be(toDo.Description);
            x.IsCompleted.Should().Be(toDo.IsCompleted);
        });
    }

    [Fact]
    public async Task ItShouldNotCreateAToDoWithEmptyTitle()
    {
        var toDo = new CreateToDoCommand("", "Test description", false);
        var sut = GetCreateToDosHandler();

        var result = await sut.HandleAsync(toDo);
        result.IsFaulted.Should().BeTrue();
        result.IfFail(x =>
        {
            x.Should().BeOfType<ValidationException>();
            x.Message.Should().Be("Todo could not be validated.");
            var exception = (ValidationException)x;
            exception.Errors.Should().ContainSingle();
            exception.Errors.First().PropertyName.Should().Be("Title");
            exception.Errors.First().ErrorMessage.Should().Be("'Title' must not be empty.");
        });
    }

    [Fact]
    public async Task ItShouldNotCreateAToDoWithEmptyDescription()
    {
        var toDo = new CreateToDoCommand("Test", "", false);
        var sut = GetCreateToDosHandler();

        var result = await sut.HandleAsync(toDo);
        result.IsFaulted.Should().BeTrue();
        result.IfFail(x =>
        {
            x.Should().BeOfType<ValidationException>();
            x.Message.Should().Be("Todo could not be validated.");
            var exception = (ValidationException)x;
            exception.Errors.Should().ContainSingle();
            exception.Errors.First().PropertyName.Should().Be("Description");
            exception.Errors.First().ErrorMessage.Should().Be("'Description' must not be empty.");
        });
    }

    [Fact]
    public async Task ItShouldNotCreateAToDoWithEmptyTitleAndDescription()
    {
        var toDo = new CreateToDoCommand("", "", false);
        var sut = GetCreateToDosHandler();

        var result = await sut.HandleAsync(toDo);
        result.IsFaulted.Should().BeTrue();
        result.IfFail(x =>
        {
            x.Should().BeOfType<ValidationException>();
            x.Message.Should().Be("Todo could not be validated.");
            var exception = (ValidationException)x;
            exception.Errors.Should().HaveCount(2);
            exception.Errors.First().PropertyName.Should().Be("Title");
            exception.Errors.First().ErrorMessage.Should().Be("'Title' must not be empty.");
            exception.Errors.Last().PropertyName.Should().Be("Description");
            exception.Errors.Last().ErrorMessage.Should().Be("'Description' must not be empty.");
        });
    }

    private static CreateToDosHandler GetCreateToDosHandler()
    {
        var sut = new CreateToDosHandler(new CreateToDoService("createTodos"));
        return sut;
    }
}