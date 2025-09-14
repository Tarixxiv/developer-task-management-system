namespace AgileroC;

public class Assignment
{
    public required Developer Developer {get; set;}
    public required Task Task { get; set; }
    public DateOnly AssignedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

}