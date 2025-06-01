namespace RiverCompiler.Output;

public class Output
{
    public static void Show(ConsoleColor color, params string[] messages)
    {
        Console.ForegroundColor = color;

        messages
            .ToList()
            .ForEach(msg =>  Console.WriteLine(msg));

        Console.ResetColor();
    }
}
