using FluentAssertions;
using ToDos.Application.CreateToDos;
using ToDos.Application.ToDoListing;

namespace ToDos.Application.Tests.ToDos.ToDoListing;

public class ToDoListingHandlerTests : IAsyncLifetime
{
    private readonly string _directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "TestToDos");

    [Fact]
    public async Task GivenNoToDos_ReturnsEmptyList()
    {
        var handler = new ToDoListingHandler(new ToDoListingService("emptyPath"));
        var result = await handler.HandleAsync(new ToDoListingQuery());

        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GivenToDos_ReturnsToDoList()
    {
        var handler = new ToDoListingHandler(new ToDoListingService(_directory));
        var result = await handler.HandleAsync(new ToDoListingQuery());

        result.Should().NotBeEmpty();
    }

    public async Task InitializeAsync()
    {
        if (!Directory.Exists(_directory)) Directory.CreateDirectory(_directory);
        if (Directory.GetFiles(_directory) is { Length: <= 0 })
        {
            var createHandler = new CreateToDosHandler(new CreateToDoService(_directory));
            await createHandler.HandleAsync(new CreateToDoCommand("Test", "Test", false));
        }
    }

    public Task DisposeAsync()
    {
        Directory.Delete(_directory, true);
        return Task.CompletedTask;
    }
}