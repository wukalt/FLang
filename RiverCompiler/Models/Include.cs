namespace RiverCompiler.Models;

public class Include
{
    public List<string> FileFullPathes { get; set; } = new();

    public Include(List<string> pathes)
    {
        FileFullPathes = pathes;
    }
}
