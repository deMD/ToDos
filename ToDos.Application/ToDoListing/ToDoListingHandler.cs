using System.Text.Json;
using ToDos.Application.CreateToDos;

namespace ToDos.Application.ToDoListing;

public class ToDoListingHandler(ToDoListingService service)
{
    public async Task<IEnumerable<object>> HandleAsync(ToDoListingQuery toDoListingQuery)
    {
        return await Task.FromResult(service.GetToDos());
    }
}

public class ToDoListingService(string basePath)
{   
    public IEnumerable<ToDoDto> GetToDos()
    {
        if (!Directory.Exists(basePath))
        {
            return [];
        }
        
        var todosPaths = Directory.GetFiles(basePath);
        
        var toDos = todosPaths
            .Select(File.ReadAllText)
            .Select(json => JsonSerializer.Deserialize<ToDoDto>(json)!);
        
        return toDos;
    }
}

public record ToDoDto(int Id, string Title, string Description, bool IsCompleted);