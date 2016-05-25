using UnityEngine;
using UnityEngine.UI;

public class UITaskRemembrance : MonoBehaviour
{
    private const string MeetingTitle = "Tasks Remembered";
    private const string TaskObjective = "Unknown Objective";
    private const string TaskDescription = "Unknown Description";

    [Tooltip("text where task to be remembered will be input")]
    [SerializeField]
    private Text taskToRemember;

    [Tooltip("Button pushed to remember task")]
    [SerializeField]
    private Button save;

    private Meeting rememberedMeeting;

    /// <summary>
    /// Saves the task to a newly created meeting
    /// </summary>
    public void RememberTask()
    {
        if (string.IsNullOrEmpty(taskToRemember.text))
        {
            return;
        }
        if (rememberedMeeting == null)
        {
            rememberedMeeting = new Meeting(MeetingTitle);
            ScheduleManager.Instance.AddMeeting(rememberedMeeting);
        }
        var task = new Task(taskToRemember.text, TaskObjective, TaskDescription, 0);
        rememberedMeeting.AddTask(task);
        Debug.Log("BEFORE: " + taskToRemember.text);
        taskToRemember.text = "";
        Debug.Log("AFTER: " + taskToRemember.text);
    }
}