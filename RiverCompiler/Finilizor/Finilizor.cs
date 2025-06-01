using RiverCompiler.Models;
using RiverCompiler.Output;
using RiverCompiler.Parser;

namespace RiverCompiler.Finilizor;

public class Finilizor
{
    public static string Finilize
         (List<string> includesInput, string controlInput, List<string> casesInput, List<string> functionsInput)

    {

        var parsedIncludes = IncludeParser.ParseInclude(includesInput.ToArray());
        var parsedControl = ControlParser.ParseControl(controlInput);
        var parsedCases = CasesParser.ParseCase(casesInput.ToArray());
        List<FunctionSign> functionSigns = new();
        List<string> FunctionBodies = new();

        FunctionSign sign;
        foreach (var function in functionsInput)
        {
            string funcHead = function.Trim().Split("\r\n")[0];
            string funcBody = function.Replace(funcHead, "").Trim().TrimEnd('}');

            sign = FunctionParser.ParseFunctionSign(funcHead);
            functionSigns.Add(sign);

            FunctionBodies.Add(funcBody);
        }

        ParseIL parseIL = new(parsedIncludes, parsedControl, parsedCases, functionSigns, FunctionBodies);
        string parsedText = parseIL.GetParsedResult();
        Writer.Write("out.cs", parsedText);

        return parsedText;
    }
}
