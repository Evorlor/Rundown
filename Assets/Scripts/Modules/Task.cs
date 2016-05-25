using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Task
{
    private const string TitleError = "ERROR: The task must have a title!";
    private const string ObjectiveError = "ERROR: The task must have an objective!";
    private const string DescriptionError = "ERROR: The task must have a description!";
    private const string DurationError = "ERROR: The task must have a duration!";

    [SerializeField]
    private string title;

    [SerializeField]
    private string objective;

    [SerializeField]
    private string description;

    [SerializeField]
    private int duration;

    /// <summary>
    /// Constructor of Tasks
    /// </summary>
    /// <param name="title">Title for task</param>
    /// <param name="objective">Objective for task</param>
    /// <param name="description">Description for task</param>
    /// <param name="duration">Duration of task</param>
    public Task(string title, string objective, string description, int duration)
    {
        this.title = title;
        this.objective = objective;
        this.description = description;
        this.duration = duration;
    }

    /// <summary>
    /// Gets the task's title
    /// </summary>
    /// <returns>Title of task</returns>
    public string GetTitle()
    {
        return title;
    }

    /// <summary>
    /// Title of the task
    /// </summary>
    /// <param name="title">Task's new title</param>
    public void SetTitle(string title)
    {
        this.title = title;
    }

    /// <summary>
    /// Gets the task's objective
    /// </summary>
    /// <returns>Objective of the task</returns>
    public string GetObjective()
    {
        return objective;
    }

    /// <summary>
    /// Objective of the task
    /// </summary>
    /// <param name="objective">Task's new objective</param>
    public void SetObjective(string objective)
    {
        this.objective = objective;
    }

    /// <summary>
    /// Gets the task's detailed description
    /// </summary>
    /// <returns>description of the task</returns>
    public string GetDescription()
    {
        return description;
    }

    /// <summary>
    /// Description of the task
    /// </summary>
    /// <param name="description">Task's new description</param>
    public void SetDescription(string description)
    {
        this.description = description;
    }

    /// <summary>
    /// Gets the task's length in minutes
    /// </summary>
    /// <returns>Duration of task</returns>
    public int GetDuration()
    {
        return duration;
    }

    /// <summary>
    /// Duration for the task in minutes
    /// </summary>
    /// <param name="time">Task's new duration</param>
    public void SetDuration(int time)
    {
        this.duration = time;
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
    /// Checks if the subTitle attribute is valid
    /// </summary>
    /// <returns>Whether or not the subTitle attribute is valid</returns>
    public bool IsValidObjective()
    {
        return !string.IsNullOrEmpty(objective);
    }

    /// <summary>
    /// Checks if the description attribute is valid
    /// </summary>
    /// <returns>Whether or not the description attribute is valid</returns>
    public bool IsValidDescription()
    {
        return !string.IsNullOrEmpty(description);
    }

    /// <summary>
    /// Checks if the time attribute is valid
    /// </summary>
    /// <returns>Whether or not the time attribute is valid</returns>
    public bool IsValidTime()
    {
        return duration > 0;
    }

    /// <summary>
    /// Checks if all task attributes are valid
    /// </summary>
    /// <returns>Whether or not all task attributes are valid</returns>
    public bool IsValid()
    {
        return IsValidTitle() && IsValidObjective() && IsValidDescription() && IsValidTime();
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
        if (!IsValidObjective())
        {
            errors.Add(ObjectiveError);
        }
        if (!IsValidDescription())
        {
            errors.Add(DescriptionError);
        }
        if (!IsValidTime())
        {
            errors.Add(DurationError);
        }
        return errors.ToArray();
    }
}