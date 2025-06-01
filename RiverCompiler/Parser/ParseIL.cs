using System.Text;
using RiverCompiler.Linker;
using RiverCompiler.Models;

namespace RiverCompiler.Parser;

public class ParseIL
{
    private readonly StringBuilder _resultBuilder;
    private readonly Include _include;
    private readonly Control _control;
    private readonly Case _case;
    private readonly List<FunctionSign> _funcSigns;
    private readonly List<string> _funcBodies;

    public ParseIL(Include inc, Control control, Case cas, List<FunctionSign> funcSigns, List<string> funcBodies)
    {
        _resultBuilder = new();
        _include = inc;
        _control = control;
        _case = cas;
        _funcSigns = funcSigns;
        _funcBodies = funcBodies;
    }

    public string GetParsedResult()
    {
        _resultBuilder.Append(Injector.GetLibs());
        _resultBuilder.Append(Injector.GetMainStyle());
        ParseControlToIL(_control);
        _resultBuilder.Append("}");
        ParseFunctionSignsToIL(_funcSigns, _funcBodies);
        ParseIncludeToIL(_include);
        _resultBuilder.Append("}");
        return _resultBuilder.ToString();
    }


    private void ParseControlToIL(Control control)
    {
        _resultBuilder.Append("args.ToList()\r\n.ForEach(x =>\r\n{");
        _resultBuilder.Append(GenerateCaseStatement(_case));
        _resultBuilder.Append("});");
    }

    private string GenerateCaseStatement(Case caseModel)
    {
        StringBuilder localBuilder = new();
        localBuilder.Append($"switch ({caseModel.CaseName})");
        localBuilder.Append("{");

        foreach (var pair in caseModel.CaseAndCallingFunctionName)
        {
            localBuilder.Append($"case \"{pair.Key}\": {pair.Value};\n break;\n");
        }

        localBuilder.Append("default:\nthrow new InvalidOperationException(\"the arg is not valid\");");
        localBuilder.Append("}");

        return localBuilder.ToString();
    }

    private void ParseFunctionSignsToIL(List<FunctionSign> funcSign, List<string> functionBodies)
    {
        int index = 0;

        foreach (var sign in funcSign)
        {

            _resultBuilder.Append($"\n{sign.AccessModifier} static {sign.ReturnType} {sign.FunctionName}\n");

            _resultBuilder.Append("(");
            AddFunctionArgs(sign);
            _resultBuilder.Append(")");

            _resultBuilder.Append("{");
            _resultBuilder.Append(functionBodies[index++]);
            _resultBuilder.Append("}");
        }
    }

    private void AddFunctionArgs(FunctionSign sign)
    {
        int length = default;
        foreach (var arg in sign.InputsPairs)
        {
            _resultBuilder.Append($"{arg.Value} {arg.Key}");
            length++;

            if (sign.InputsPairs.Keys.Count != length)
            {
                _resultBuilder.Append(",");
            }
        }
    }

    private void ParseIncludeToIL(Include include)
    {
        foreach (string path in include.FileFullPathes)
        {
            _resultBuilder.Append(File.ReadAllText(path));
        }
    }
}
