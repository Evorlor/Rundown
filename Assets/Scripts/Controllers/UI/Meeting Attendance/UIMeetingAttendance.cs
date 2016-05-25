using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMeetingAttendance : MonoBehaviour
{
    private const string ClockFormat = "{0}:{1:00}";

    [Tooltip("Timer which countdown the time remaining on the current task")]
    [SerializeField]
    private Text timer;

    [Tooltip("Title for current task")]
    [SerializeField]
    private Text title;

    [Tooltip("Objective for current task")]
    [SerializeField]
    private Text objective;

    [Tooltip("Description for current task")]
    [SerializeField]
    private Text description;

    [Tooltip("Title of upcoming task")]
    [SerializeField]
    private Text upcomingTask;

    private Meeting meeting;
    private float timeRemainingOnCurrentTask;

    void Awake()
    {
        meeting = ScheduleManager.Instance.GetActiveMeeting();
    }

    void Start()
    {
        UpdateTask();
    }

    void Update()
    {
        timeRemainingOnCurrentTask -= Time.deltaTime;
        if (timeRemainingOnCurrentTask <= 0)
        {
            UpdateTask();
        }
        UpdateTimeRemainingUI();
    }

    void OnApplicationQuit()
    {
        FinishMeeting();
    }

    /// <summary>
    /// Returns to the main menu
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
    }

    /// <summary>
    /// Finishes a task early
    /// </summary>
    public void FinishTask()
    {
        UpdateTask();
    }

    private void UpdateTimeRemainingUI()
    {
        var timeSpan = TimeSpan.FromSeconds(timeRemainingOnCurrentTask);
        var time = string.Format(ClockFormat, (int)timeSpan.TotalMinutes, timeSpan.Seconds);
        timer.text = time;
    }

    private void UpdateTask()
    {
        if (meeting.GetTasks().Count == 0)
        {
            FinishMeeting();
            return;
        }
        var currentTask = meeting.GetTasks()[0];
        timeRemainingOnCurrentTask = currentTask.GetDuration();
        ShowCurrentTask(currentTask);
        meeting.RemoveTask(currentTask);
        var upcomingTask = meeting.GetTasks().Count > 0 ? meeting.GetTasks()[0] : null;
        ShowFutureTask(upcomingTask);
    }

    private void FinishMeeting()
    {
        if (meeting.GetTasks().Count == 0)
        {
            ScheduleManager.Instance.RemoveMeeting(meeting);
        }
        ScheduleManager.Instance.SaveSchedule();
        SceneManager.LoadScene(SceneNames.MainMenu);
    }

    private void ShowCurrentTask(Task task)
    {
        title.text = task.GetTitle();
        objective.text = task.GetObjective();
        description.text = task.GetDescription();
    }

    private void ShowFutureTask(Task task)
    {
        upcomingTask.text = task != null ? task.GetTitle() : "";
    }
}