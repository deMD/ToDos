using System.Text.Json;
using LanguageExt.Common;

namespace ToDos.Application.CreateToDos;

public class CreateToDoService
{
    public async Task<Result<ToDo>> SaveToDoAsync(ToDo toDo)
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ToDos");
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        var files = Directory.GetFiles(directory);

        toDo.SetId(files.Length + 1);

        var filePath = Path.Combine(directory, $"{toDo.Id}.json");
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(toDo));

        return toDo;
    }
}