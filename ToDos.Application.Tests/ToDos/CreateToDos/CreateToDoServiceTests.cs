using System.Text.Json;
using FluentAssertions;
using LanguageExt;
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
        result.Should().Be(Unit.Default);
        toDo.Id.Should().BePositive();
        
    }

    [Fact]
    public async Task ItTracksOutput()
    {
        var toDo = new ToDo("Test", "Test description", false);

        var sut = new CreateToDoService();
        var output = sut.TrackOutput();
        await sut.SaveToDoAsync(toDo);

        output.Data().Should().NotBeEmpty();
        output.Data().First().Should().BeEquivalentTo(JsonSerializer.Serialize(toDo));
    }
}