namespace RiverCompiler.Models;

public class Case
{
    public string CaseName { get; set; } = string.Empty;
    public Dictionary<string, string> CaseAndCallingFunctionName { get; set; } = new();

    public Case(string caseName, Dictionary<string, string> caseAndCallingFunctionName)
    {
        CaseName = caseName;
        CaseAndCallingFunctionName = caseAndCallingFunctionName;
    }
}
