namespace RiverCompiler.Linker;

public class Injector
{
    public static string GetLibs()
    {
        return "using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing System.Text;\r\nusing System.Threading.Tasks;";
    }

    public static string GetMainStyle()
    {
        return "class Program\r\n{\r\n    public static void Main(string[] args)\r\n    {\r\n";
    }
}