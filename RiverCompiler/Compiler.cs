using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Diagnostics;
using System.Reflection;

namespace RiverCompiler;

public class Compiler
{
    public static void Compile(string filePath)
    {
        string source = Linker.Linker.Link(filePath);

        string dotnetRefPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "dotnet", "packs", "Microsoft.NETCore.App.Ref", "9.0.5", "ref", "net9.0"
        );

        if (!Directory.Exists(dotnetRefPath))
        {
            Console.WriteLine("❌ Reference pack for .NET 9.0.5 not found!");
            return;
        }

        var references = Directory
            .GetFiles(dotnetRefPath, "*.dll")
            .Select(file => MetadataReference.CreateFromFile(file))
            .ToList();

        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        var compilation = CSharpCompilation.Create(
            "GeneratedProgram",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.ConsoleApplication)
        );

        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedProgram.exe");

        EmitResult result = compilation.Emit(outputPath);

        if (result.Success)
        {
            Console.WriteLine("Compilation successful!");
            Console.WriteLine($"Output: {outputPath}");
        }
        else
        {
            Console.WriteLine("Compilation failed:");
            foreach (var diagnostic in result.Diagnostics)
            {
                Console.WriteLine(diagnostic.ToString());
            }
        }
    }
}
