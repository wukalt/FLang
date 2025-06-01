namespace RiverCompiler.Models;

public class FunctionSign
{
    public string AccessModifier { get; set; } = string.Empty;
    public string FunctionName { get; set; } = string.Empty;
    public string ReturnType { get; set; } = string.Empty;
    public Dictionary<string, string> InputsPairs { get; set; } = new();

    public FunctionSign(string accessModifier,
        string functionName,
        string returnType,
        Dictionary<string, string> inputs)

    {
        AccessModifier = accessModifier;
        FunctionName = functionName;
        ReturnType = returnType;
        InputsPairs = inputs;
    }
}