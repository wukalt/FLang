using RiverCompiler.Models;

namespace RiverCompiler.Parser;

public class CasesParser
{
    public static Case ParseCase(string[] lines)
    {
        var caseName = FindCaseName(lines);
        var caseAndCallingFunctionName = FindFunctionNames(lines);

        return new Case(caseName, caseAndCallingFunctionName);
    }

    private static string FindCaseName(string[] headerLine)
    {
        return headerLine[0]
            .Replace("<", "")
            .Replace(">", "")
            .Replace("{", "")
            .Replace("-", "")
            .Trim();
    }

    private static Dictionary<string, string> FindFunctionNames(string[] lines)
    {
        Dictionary<string, string> pairs = new();

        for (int i = 1; i < lines.Length - 1; i++)
        {
            string line = lines[i];
            var caseAndCallingFunctionName = line.Split("=>");
            caseAndCallingFunctionName[0] = caseAndCallingFunctionName[0]
                .Replace("\"", "")
                .Trim();

            pairs[caseAndCallingFunctionName[0]] = caseAndCallingFunctionName[1].Trim();
        }

        return pairs;
    }
}
