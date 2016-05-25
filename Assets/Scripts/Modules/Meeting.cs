using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Meeting
{
    private const string TitleError = "ERROR: The meeting must have a title!";

    [SerializeField]
    private string title;

    [SerializeField]
    private List<Task> tasks = new List<Task>();

    private Task activeTask;

    public Meeting(string title)
    {
        this.title = title;
    }

    /// <summary>
    /// Checks if the title is valid
    /// </summary>
    /// <returns>Whether or not the title attribute is valid</returns>
    public bool IsValidTitle()
    {
        return !string.IsNullOrEmpty(title);
    }

    /// <summary>
    /// Checks if all meeting attributes are valid
    /// </summary>
    /// <returns>Whether or not all meeting attributes are valid</returns>
    public bool IsValid()
    {
        return IsValidTitle();
    }

    /// <summary>
    /// Gets the title of the meeting
    /// </summary>
    /// <returns>Meeting title</returns>
    public string GetTitle()
    {
        return title;
    }

    /// <summary>
    /// Sets the title for the meeting
    /// </summary>
    /// <param name="title">Meeting title</param>
    public void SetTitle(string title)
    {
        this.title = title;
    }

    /// <summary>
    /// Gets a list of all of this meeting's tasks
    /// </summary>
    /// <returns>List of tasks</returns>
    public List<Task> GetTasks()
    {
        return tasks;
    }

    /// <summary>
    /// Adds a task to the meeting
    /// </summary>
    /// <param name="task">Task to be added</param>
    /// <returns>Whether or not it was successful</returns>
    public bool AddTask(Task task)
    {
        if (task.IsValid() && !tasks.Contains(task))
        {
            tasks.Add(task);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the currently active task
    /// </summary>
    /// <returns>Currently active task</returns>
    public Task GetActiveTask()
    {
        return activeTask;
    }

    /// <summary>
    /// Sets the currently active task
    /// </summary>
    /// <param name="task">Currently active task</param>
    public void SetActiveTask(Task task)
    {
        activeTask = task;
    }

    /// <summary>
    /// Removes a task from this meeting
    /// </summary>
    /// <param name="task">Task to be removed</param>
    public bool RemoveTask(Task task)
    {
        return tasks.Remove(task);
    }

    /// <summary>
    /// Gets an array of all errors messages
    /// </summary>
    public string[] GetErrorMessages()
    {
        var errors = new List<string>();
        if (!IsValidTitle())
        {
            errors.Add(TitleError);
        }
        return errors.ToArray();
    }
}