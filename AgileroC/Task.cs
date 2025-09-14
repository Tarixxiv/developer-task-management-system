namespace AgileroC;

public class Task
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required int EstimatedHours { get; set; }
    public required bool IsCompleted { get; set; }
}