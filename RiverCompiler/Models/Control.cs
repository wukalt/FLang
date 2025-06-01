namespace RiverCompiler.Models;

public class Control
{
    public string ControlName { get; set; } = string.Empty;

    public Control(string controlName)
    {
        ControlName = controlName;
    }
}
