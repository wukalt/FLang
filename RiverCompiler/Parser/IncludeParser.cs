using RiverCompiler.Models;

namespace RiverCompiler.Parser;

public class IncludeParser
{
    public static Include ParseInclude(string[] lines)
    {
        List<string> pathes = new();

        foreach (string line in lines)
        {
            string path = line.
                Split(" ")[1]
                .Replace("<", "")
                .Replace(">", "");

            pathes.Add(path);
        }

        return new Include(pathes);
    }
}
