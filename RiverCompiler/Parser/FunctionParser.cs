using System.Text;
using RiverCompiler.Models;

namespace RiverCompiler.Parser;

public class FunctionParser
{
    public static FunctionSign ParseFunctionSign(string functionSign)
    {
        return new FunctionSign(FindAccessModifier(functionSign),
            FindFunctionName(functionSign),
            FindReturnType(functionSign),
            FindFunctionInputs(functionSign)
        );
    }

    private static string FindAccessModifier(string sign)
    {
        string accessModifier = sign.Split(" ")[0];

        return accessModifier switch
        {
            "gl" => "public",
            "pr" => "private",
            "inter" => "internal",
            _ => throw new InvalidCastException("access modifier is not valid")

        };
    }

    private static string FindFunctionName(string sign)
    {
        string name = sign.Split(" ")[2];
        int startIndex = name.IndexOf("(");

        return name.Remove(startIndex);
    }

    private static string FindReturnType(string sign)
    {
        // +4 -> for 'out ' 
        int returnTypeIndex = sign.IndexOf("out") + 4;
        string bussyReturnType = sign.Substring(returnTypeIndex);

        string returnType = bussyReturnType.Split(" ")[0];

        return returnType;
    }

    private static Dictionary<string, string> FindFunctionInputs(string sign)
    {
        int startIndex = sign.IndexOf("(");
        int endIndex = sign.IndexOf("out");

        string inputs = sign.Substring(startIndex, (endIndex - startIndex));

        StringBuilder builder = new();
        foreach (string input in inputs.Split(" "))
        {
            if (input.Trim().Replace("(", "").Replace(")", "") is not "in")
            {
                builder.Append($"{input} ");
            }
        }

        var typeAndName = builder.ToString().Trim().Split(",");

        Dictionary<string, string> pairs = new();
        foreach (string input in typeAndName)
        {
            var typeValues = input.Trim().Split(" ");
            if (!string.IsNullOrWhiteSpace(input))
                pairs[typeValues[1]] = typeValues[0];
        }

        return pairs;
    }
}
