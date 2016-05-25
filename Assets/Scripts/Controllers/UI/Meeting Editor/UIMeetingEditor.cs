using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMeetingEditor : MonoBehaviour
{
    private const float EmptyTaskSpace = 0.5f;
    private const string EditButtonName = "edit";
    private const string CancelButtonName = "cancel";

    [Tooltip("Title of the meeting")]
    [SerializeField]
    private InputField title;

    [Tooltip("Panel where tasks go")]
    [SerializeField]
    private RectTransform content;

    [Tooltip("Prefab to use when creating task UIs")]
    [SerializeField]
    private RectTransform taskRenderer;

    [Tooltip("Panel with message field used to display input errors")]
    [SerializeField]
    private RectTransform error;

    private Meeting meeting;

    void Awake()
    {
        meeting = ScheduleManager.Instance.GetActiveMeeting();
    }

    void Start()
    {
        error.gameObject.SetActive(false);
        InitializeMeetingInfo();
    }

    void OnApplicationQuit()
    {
        SaveMeeting();
    }

    /// <summary>
    /// Saves the meeting
    /// </summary>
    public void SaveMeeting()
    {
        if (meeting.IsValid())
        {
            ScheduleManager.Instance.SaveSchedule();
            ScheduleManager.Instance.SetActiveMeeting(null);
            SceneManager.LoadScene(SceneNames.ScheduleEditor);
        }
        else
        {
            DisplayInputErrors();
        }
    }

    /// <summary>
    /// Permanently removes the current meeting from the meetings list
    /// </summary>
    public void CancelMeeting()
    {
        ScheduleManager.Instance.RemoveMeeting(ScheduleManager.Instance.GetActiveMeeting());
        ScheduleManager.Instance.SetActiveMeeting(null);
        SceneManager.LoadScene(SceneNames.MainMenu);
    }

    /// <summary>
    /// Initializes the creation of a new task
    /// </summary>
    public void CreateTask()
    {
        if (meeting.IsValid())
        {
            SceneManager.LoadScene(SceneNames.TaskEditor);
        }
        else
        {
            DisplayInputErrors();
        }
    }

    /// <summary>
    /// Edits the title for this meeting
    /// </summary>
    /// <param name="title">Meeting's new title</param>
    public void EditTitle(string title)
    {
        meeting.SetTitle(title);
    }

    private void InitializeMeetingInfo()
    {
        foreach (Transform child in content.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        if (meeting != null)
        {
            title.text = meeting.GetTitle();
            var tasks = meeting.GetTasks();
            float contentWidth = content.sizeDelta.x;
            float contentHeight = taskRenderer.sizeDelta.y * (tasks.Count + EmptyTaskSpace);
            content.sizeDelta = new Vector2(contentWidth, contentHeight);
            for (int i = 0; i < tasks.Count; i++)
            {
                var taskRendererInstance = Instantiate(taskRenderer) as RectTransform;
                taskRendererInstance.SetParent(content);
                taskRendererInstance.anchoredPosition = new Vector2(0, -i * taskRenderer.sizeDelta.y);
                var task = tasks[i];
                SetUpTaskUI(task, taskRendererInstance.gameObject);
            }
        }
        else
        {
            CreateMeeting();
        }
    }

    private void CreateMeeting()
    {
        meeting = new Meeting("");
        ScheduleManager.Instance.AddMeeting(meeting);
        ScheduleManager.Instance.SetActiveMeeting(meeting);
        InitializeMeetingInfo();
    }

    private void SetUpTaskUI(Task task, GameObject taskInstance)
    {
        var title = taskInstance.GetComponentInChildren<Text>();
        title.text = task.GetTitle();
        var buttons = taskInstance.GetComponentsInChildren<Button>();
        var editButton = buttons.Where(b => string.Compare(b.name, EditButtonName, true) == 0).FirstOrDefault();
        var cancelButton = buttons.Where(b => string.Compare(b.name, CancelButtonName, true) == 0).FirstOrDefault();
        if (!editButton)
        {
            Debug.LogError("The edit button must have \"" + EditButtonName + "\" within its name!", taskInstance.gameObject);
        }
        if (!cancelButton)
        {
            Debug.LogError("The edit button must have \"" + CancelButtonName + "\" within its name!", taskInstance.gameObject);
        }
        editButton.onClick.AddListener(() => EditTask(task));
        cancelButton.onClick.AddListener(() => RemoveTask(task));
    }

    private void DisplayInputErrors()
    {
        var errorText = error.GetComponentInChildren<Text>();
        errorText.text = meeting.GetErrorMessages()[0];
        error.gameObject.SetActive(true);
    }

    private void EditTask(Task task)
    {
        meeting.SetActiveTask(task);
        CreateTask();
    }

    private void RemoveTask(Task task)
    {
        meeting.RemoveTask(task);
        InitializeMeetingInfo();
    }
}
