using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScheduleEditor : MonoBehaviour
{
    private const float EmptyMeetingSpace = 0.5f;
    private const string EditButtonName = "edit";
    private const string CancelButtonName = "cancel";

    [Tooltip("Panel where meetings go")]
    [SerializeField]
    private RectTransform content;

    [Tooltip("Prefab to use when creating meeting UIs")]
    [SerializeField]
    private RectTransform meetingRenderer;

    void Start()
    {
        InitializeScheduleInfo();
    }

    /// <summary>
    /// Creates a new meeting
    /// </summary>
    public void CreateMeeting()
    {
        EditMeeting(null);
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
        float contentWidth = content.sizeDelta.x;
        float contentHeight = meetingRenderer.sizeDelta.y * (meetings.Count + EmptyMeetingSpace);
        content.sizeDelta = new Vector2(contentWidth, contentHeight);
        for (int i = 0; i < meetings.Count; i++)
        {
            var meetingRendererInstance = Instantiate(meetingRenderer) as RectTransform;
            meetingRendererInstance.SetParent(content);
            meetingRendererInstance.anchoredPosition = new Vector2(0, -i * meetingRenderer.sizeDelta.y);
            var meeting = meetings[i];
            CreateMeetingUI(meeting, meetingRendererInstance.gameObject);
        }
    }

    private void CreateMeetingUI(Meeting meeting, GameObject meetingInstance)
    {
        var title = meetingInstance.GetComponentInChildren<Text>();
        title.text = meeting.GetTitle();
        var buttons = meetingInstance.GetComponentsInChildren<Button>();
        var editButton = buttons.Where(b => string.Compare(b.name, EditButtonName, true) == 0).FirstOrDefault();
        var cancelButton = buttons.Where(b => string.Compare(b.name, CancelButtonName, true) == 0).FirstOrDefault();
        if (!editButton)
        {
            Debug.LogError("The edit button must have \"" + EditButtonName + "\" within its name!", meetingInstance.gameObject);
        }
        if (!cancelButton)
        {
            Debug.LogError("The edit button must have \"" + CancelButtonName + "\" within its name!", meetingInstance.gameObject);
        }
        editButton.onClick.AddListener(() => EditMeeting(meeting));
        cancelButton.onClick.AddListener(() => CancelMeeting(meeting));
    }

    private void EditMeeting(Meeting meeting)
    {
        ScheduleManager.Instance.SetActiveMeeting(meeting);
        SceneManager.LoadScene(SceneNames.MeetingEditor);
    }

    private void CancelMeeting(Meeting meeting)
    {
        ScheduleManager.Instance.RemoveMeeting(meeting);
        InitializeScheduleInfo();
    }
}