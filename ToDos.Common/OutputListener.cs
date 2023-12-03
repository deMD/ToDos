namespace ToDos.Common;

public class OutputListener<T>
{
    private readonly List<OutputTracker<T>> _listeners = new();

    public void Track(T data)
    {
        _listeners.ForEach(x => x.Add(data));
    }

    public OutputTracker<T> CreateTracker()
    {
        var tracker = new OutputTracker<T>(this);
        _listeners.Add(tracker);
        return tracker;
    }
    
    public void Remove(OutputTracker<T> tracker)
    {
        _listeners.Remove(tracker);
    }
}

public class OutputTracker<T>(OutputListener<T> listener)
{
    private readonly List<T> _output = new();

    public void Add(T data)
    {
        _output.Add(data);
    }

    public void Remove()
    {
        listener.Remove(this);
    }

    public IEnumerable<T> Data()
    {
        return new List<T>(_output);
    }
}