using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITaskEditor : MonoBehaviour
{
    [Tooltip("Title for the task")]
    [SerializeField]
    private InputField title;

    [Tooltip("Name for the task")]
    [SerializeField]
    private InputField objective;

    [Tooltip("Description for the task")]
    [SerializeField]
    private InputField description;

    [Tooltip("How long the task will take in minutes")]
    [SerializeField]
    private InputField time;

    [Tooltip("Panel with message field used to display input errors")]
    [SerializeField]
    private RectTransform error;

    private Meeting meeting = ScheduleManager.Instance.GetActiveMeeting();

    void Awake()
    {
        InitializeTaskInfo();
    }

    void Start()
    {
        error.gameObject.SetActive(false);
    }

    void OnApplicationQuit()
    {
        ScheduleManager.Instance.SetActiveMeeting(null);
        CancelTask();
    }

    /// <summary>
    /// Updates the active task's title
    /// </summary>
    /// <param name="title">Task's new title</param>
    public void EditTitle(string title)
    {
        meeting.GetActiveTask().SetTitle(title);
    }


    /// <summary>
    /// Updates the active task's objective
    /// </summary>
    /// <param name="objective">Task's new objective</param>
    public void EditObjective(string objective)
    {
        meeting.GetActiveTask().SetObjective(objective);
    }

    /// <summary>
    /// Updates the active task's description
    /// </summary>
    /// <param name="description">Task's new description</param>
    public void EditDescription(string description)
    {
        meeting.GetActiveTask().SetDescription(description);
    }

    /// <summary>
    /// Updates the active task's duration
    /// </summary>
    /// <param name="duration">Task's new duration</param>
    public void EditDuration(string duration)
    {
        int minutes;
        bool validDuration = int.TryParse(duration, out minutes);
        if (validDuration)
        {
            meeting.GetActiveTask().SetDuration(minutes);
        }
    }

    /// <summary>
    /// Adds a task
    /// </summary>
    public void SaveTask()
    {
        bool valid = meeting.GetActiveTask().IsValid();
        if (valid)
        {
            meeting.AddTask(meeting.GetActiveTask());
            meeting.SetActiveTask(null);
            SceneManager.LoadScene(SceneNames.MeetingEditor);
        }
        else
        {
            DisplayInputErrors();
        }
    }

    /// <summary>
    /// Deletes the task
    /// </summary>
    public void CancelTask()
    {
        meeting.RemoveTask(meeting.GetActiveTask());
        meeting.SetActiveTask(null);
        SceneManager.LoadScene(SceneNames.MeetingEditor);
    }

    private void DisplayInputErrors()
    {
        var errorText = error.GetComponentInChildren<Text>();
        errorText.text = meeting.GetActiveTask().GetErrorMessages()[0];
        error.gameObject.SetActive(true);
    }

    private void InitializeTaskInfo()
    {
        if (meeting.GetActiveTask() != null)
        {
            title.text = meeting.GetActiveTask().GetTitle();
            objective.text = meeting.GetActiveTask().GetObjective();
            description.text = meeting.GetActiveTask().GetDescription();
            time.text = meeting.GetActiveTask().GetDuration().ToString();
        }
        else
        {
            var task = new Task("", "", "", 0);
            meeting.AddTask(task);
            meeting.SetActiveTask(task);
        }
    }
}