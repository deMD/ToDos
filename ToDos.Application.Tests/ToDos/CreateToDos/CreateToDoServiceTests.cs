using FluentAssertions;
using ToDos.Application.CreateToDos;

namespace ToDos.Application.Tests.ToDos.CreateToDos;

public class CreateToDoServiceTests
{
    [Fact]
    public async Task ItSavesTheToDo()
    {
        var toDo = new ToDo("Test", "Test description", false);

        var sut = new CreateToDoService();

        var result = await sut.SaveToDoAsync(toDo);
        result.IsSuccess.Should().BeTrue();
        result.IfSucc(x =>
        {
            x.Id.Should().BePositive();
            x.Title.Should().Be(toDo.Title);
            x.Description.Should().Be(toDo.Description);
            x.IsCompleted.Should().Be(toDo.IsCompleted);
        });
    }
}