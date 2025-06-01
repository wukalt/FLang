using RiverCompiler.Models;

namespace RiverCompiler.Parser;

public class ControlParser
{
    public static Control ParseControl(string line)
    {
        int startIndex = line.IndexOf("<");
        int endIndex = line.LastIndexOf(">");
        string controlName = line.Substring(++startIndex, (endIndex - startIndex));

        return new Control(controlName);
    }
}
