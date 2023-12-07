using System.Text.Json;
using LanguageExt;
using ToDos.Common;

namespace ToDos.Application.CreateToDos;

public class CreateToDoService(string saveDirectory)
{
    private readonly OutputListener<string> _outputListener = new();

    public async Task<Unit> SaveToDoAsync(ToDo toDo)
    {
        
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            saveDirectory);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        var files = Directory.GetFiles(directory);

        toDo.SetId(files.Length + 1);

        var filePath = Path.Combine(directory, $"{toDo.Id}.json");
        var contents = JsonSerializer.Serialize(toDo);
        await File.WriteAllTextAsync(filePath, contents);
        _outputListener.Track(contents);

        return Unit.Default;
    }

    public OutputTracker<string> TrackOutput()
    {
        return _outputListener.CreateTracker();
    }
}