namespace AgileroC;
/*
 Because of the peculiar way that the excersise was constructed (GetOverdueTasks and CalculateAverageTaskDurationPerSeniority having
 assignments as parameter, we end up with a stateless object with pure static methods. If that was not a requirement
 I'd make TaskManager contain a reference to some Assignment collection (injected in constructor) and then have all those methods work on that.
 I'd also change parameter type in GetOverdueTasks and CalculateAverageTaskDurationPerSeniority to IEnumerable<Assignment>,
 for the purpose of foreach loop we don't need a finalized collection.
 GetOverdueTasks would also benefit from this, as such approach would make this method lazy.
*/
public class TaskManager
{
    public static Assignment AssignTask(Task task, Developer developer)
    {
        return new Assignment
        {
            Developer = developer,
            Task = task,
        };
    }

    public static List<Task> GetOverdueTasks(List<Assignment> assignments, int daysThreshold)
    {
        var result = new List<Task>();
        foreach (var assignment in assignments)
        {
            if (assignment.Task.IsCompleted) continue;
            
            var daysPassed = CountTaskDuration(assignment);
            if (daysPassed >= daysThreshold)
            {
                result.Add(assignment.Task);
            }
        }
        return result;
    }

    public static Dictionary<string, double> CalculateAverageTaskDurationPerSeniority(List<Assignment> assignments)
    {
        var taskDurations = new Dictionary<string, double>();
        var taskCounts = new Dictionary<string, int>();

        
        foreach (var assignment in assignments)
        {
            var seniority = assignment.Developer.Seniority.ToString();
            if (taskDurations.TryAdd(seniority, 0))
            {
                taskCounts[seniority] = 0;
            }
            taskDurations[seniority] += CountTaskDuration(assignment);
            taskCounts[seniority]++;
        }

        foreach (var pair in taskCounts)
        {
            taskDurations[pair.Key] /= pair.Value;
        }
        
        return taskDurations;
    }

    private static int CountTaskDuration(Assignment assignment)
    {
        return DateOnly.FromDateTime(DateTime.Now).DayNumber - assignment.AssignedDate.DayNumber;
    }
    
}