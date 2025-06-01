namespace RiverCompiler.Output;

public class Writer
{
    public static void Write(string outputPath, string content)
    {
        File.Create(outputPath).Dispose();
        File.WriteAllText(outputPath, content);
    }
}
