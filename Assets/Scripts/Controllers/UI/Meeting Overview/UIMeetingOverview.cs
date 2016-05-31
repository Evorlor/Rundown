using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMeetingOverview : MonoBehaviour
{
    private const float EmptyAttendanceSpace = 0.5f;
    private const string AttendButtonName = "attend";

    [Tooltip("Panel where attendances go")]
    [SerializeField]
    private RectTransform content;

    [Tooltip("Prefab to use when creating attendance UIs")]
    [SerializeField]
    private RectTransform attendanceRenderer;

    void Start()
    {
        InitializeScheduleInfo();
    }

    /// <summary>
    /// Creates a new meeting
    /// </summary>
    public void CreateMeeting()
    {
        ScheduleManager.Instance.SetActiveMeeting(null);
        SceneManager.LoadScene(SceneNames.MeetingEditor);
    }

    /// <summary>
    /// Goes to the main menu
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
    }

    private void InitializeScheduleInfo()
    {
        foreach (Transform child in content.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        var meetings = ScheduleManager.Instance.GetMeetings();
        var validMeetings = meetings.Where(m => m.IsValid()).ToArray();
        float contentWidth = content.sizeDelta.x;
        float contentHeight = attendanceRenderer.sizeDelta.y * (validMeetings.Length + EmptyAttendanceSpace);
        content.sizeDelta = new Vector2(contentWidth, contentHeight);
        for (int i = 0; i < validMeetings.Length; i++)
        {
            var attendanceRendererInstance = Instantiate(attendanceRenderer) as RectTransform;
            attendanceRendererInstance.SetParent(content);
            attendanceRendererInstance.anchoredPosition = new Vector2(0, -i * attendanceRenderer.sizeDelta.y);
            var meeting = validMeetings[i];
            CreateMeetingUI(meeting, attendanceRendererInstance.gameObject);
        }
    }

    private void CreateMeetingUI(Meeting meeting, GameObject attendanceInstance)
    {
        var title = attendanceInstance.GetComponentInChildren<Text>();
        title.text = meeting.GetTitle();
        var buttons = attendanceInstance.GetComponentsInChildren<Button>();
        var attendButton = buttons.Where(b => string.Compare(b.name, AttendButtonName, true) == 0).FirstOrDefault();
        if (!attendButton)
        {
            Debug.LogError("The edit button must have \"" + AttendButtonName + "\" within its name!", attendanceInstance.gameObject);
        }
        attendButton.onClick.AddListener(() => AttendMeeting(meeting));
    }

    private void AttendMeeting(Meeting meeting)
    {
        ScheduleManager.Instance.SetActiveMeeting(meeting);
        SceneManager.LoadScene(SceneNames.MeetingAttendance);
    }
}