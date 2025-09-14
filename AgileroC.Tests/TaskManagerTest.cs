using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Xunit;

namespace AgileroC.Tests;

[TestSubject(typeof(TaskManager))]
public class TaskManagerTest
{
    private const string Name = "name";
    private const string Title = "title";
    
    private static Task CreateTask(bool isCompleted = false)
    {
        return new Task
        {
            Id = 0,
            Title = Title,
            EstimatedHours = 0,
            IsCompleted = isCompleted
        };
    }

    private static Developer CreateDeveloper(Seniority seniority)
    {
        return new Developer { Id = 1, Name = Name, Seniority = seniority };
    }
    
    private static Assignment CreateAssignment(Task task, Developer dev, DateOnly assignedDate)
    {
        return new Assignment
        {
            Task = task,
            Developer = dev,
            AssignedDate = assignedDate
        };
    }
    
    [Fact]
    public void AssignTask()
    {
        var developer = CreateDeveloper(Seniority.Junior);
        var task = CreateTask();
        
        var assignment = TaskManager.AssignTask(task, developer);
        
        Assert.Equal(DateOnly.FromDateTime(DateTime.Now), assignment.AssignedDate);
        Assert.Equal(developer, assignment.Developer);
        Assert.Equal(task, assignment.Task);
    }

    [Fact]
    public void CalculateAverageTaskDurationPerSeniority_Empty()
    {
        var result = TaskManager.CalculateAverageTaskDurationPerSeniority([]);
        Assert.Empty(result);
    }
    
    [Fact]
    public void CalculateAverageTaskDurationPerSeniority_CorrectAverages()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var assignments = new List<Assignment>
        {
            CreateAssignment(CreateTask(), CreateDeveloper(Seniority.Junior), today.AddDays(-2)),
            CreateAssignment(CreateTask(), CreateDeveloper(Seniority.Junior), today.AddDays(-4)),
            CreateAssignment(CreateTask(), CreateDeveloper(Seniority.Mid), today.AddDays(-6))
        };

        var result = TaskManager.CalculateAverageTaskDurationPerSeniority(assignments);

        Assert.Equal(2, result.Count);
        Assert.Equal(3, result[nameof(Seniority.Junior)]);
        Assert.Equal(6, result[nameof(Seniority.Mid)]);
    }
    
    [Fact]
    public void GetOverdueTasks_SkipsCompletedTasks()
    {
        var completedTask = CreateTask(true);
        var assignments = new List<Assignment>
        {
            TaskManager.AssignTask(completedTask, CreateDeveloper(Seniority.Junior))
        };

        var result = TaskManager.GetOverdueTasks(assignments, 5);

        Assert.Empty(result);
    }

    [Fact]
    public void GetOverdueTasks_Empty()
    {
        Assert.Empty(TaskManager.GetOverdueTasks([], 1));
    }
    
    [Fact]
    public void GetOverdueTasks_ReturnsTasksOverThreshold()
    {
        var task1 = CreateTask();
        var task2 = CreateTask();
        var assignments = new List<Assignment>
        {
            CreateAssignment(task1, CreateDeveloper(Seniority.Junior), DateOnly.FromDateTime(DateTime.Now.AddDays(-10))),
            CreateAssignment(task2, CreateDeveloper(Seniority.Senior), DateOnly.FromDateTime(DateTime.Now.AddDays(-2)))
        };

        var result = TaskManager.GetOverdueTasks(assignments, 5);

        Assert.Single(result);
        Assert.Contains(task1, result);
        Assert.DoesNotContain(task2, result);
    }
}