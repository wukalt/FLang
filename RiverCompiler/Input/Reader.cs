namespace RiverCompiler.Input;

public class Reader 
{
    private readonly string _path;

    public Reader(string path)
    {
        _path = path;
    }

    public string GetText()
    {
        return File.ReadAllText(_path);
    }

    public string[] GetLinesArray()
    {
        return File.ReadAllLines(_path);
    }
}
