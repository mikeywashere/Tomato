namespace Tomato
{
    public interface IPomodoroProgress
    {
        string Name { get; set; }
        int Percentage { get; set; }
    }
}