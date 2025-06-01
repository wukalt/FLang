using RiverCompiler.Input;

namespace RiverCompiler.Linker;

public class Linker
{
    public static string Link(string filePath)
    {
        Reader reader = new(filePath);

        var includes = FindIncludes(reader.GetLinesArray());
        var control = FindControl(reader.GetLinesArray());
        var cases = FindCases(reader.GetLinesArray());
        var functions = FindFunctions(reader.GetText());

        return Finilizor.Finilizor.Finilize(includes, control, cases, functions);
    }

    private static List<string> FindIncludes(string[] lines)
    {
        List<string> result = new();

        foreach (var line in lines)
        {
            if (line.Trim().StartsWith("include <"))
            {
                result.Append(line);
            }
            else
            {
                break;
            }
        }

        return result;
    }

    private static string FindControl(string[] lines)
    {
        foreach (var line in lines)
        {
            if (line.Trim().StartsWith("args => ()")
                && line.Contains("control"))
            {
                return line;
            }
        }

        throw new Exception("control was not found.");
    }

    private static List<string> FindCases(string[] lines)
    {
        int startIndex = default;
        int endIndex = default;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("->"))
            {
                startIndex = i;
            }
            else if (lines[i].Trim() is "}")
            {
                endIndex = i;
                break;
            }
        }

        endIndex++;

        return lines[startIndex..endIndex].ToList();
    }

    private static List<string> FindFunctions(string lines)
    {
        List<string> functions = new();

        int startIndexGL = lines.IndexOf("gl");
        int startIndexPR = lines.IndexOf("pr");
        int startIndexINTER = lines.IndexOf("inter");

        int minIndex = new[] { startIndexGL, startIndexPR, startIndexINTER }.Where(x => x > 0).Min();

        string result = lines.Substring(minIndex);

        foreach (string singleFunction in result.Split("\r\n\r\n"))
        {
            functions.Add(singleFunction);
        }

        return functions;
    }
}
