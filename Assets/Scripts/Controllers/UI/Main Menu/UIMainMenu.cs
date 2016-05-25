using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    /// <summary>
    /// Creates a new meeting
    /// </summary>
    public void Create()
    {
        SceneManager.LoadScene(SceneNames.MeetingEditor);
    }

    /// <summary>
    /// Edits an existing meeting
    /// </summary>
    public void Edit()
    {
        SceneManager.LoadScene(SceneNames.ScheduleEditor);
    }

    /// <summary>
    /// Attends an existing meeting
    /// </summary>
    public void Attend()
    {
        SceneManager.LoadScene(SceneNames.MeetingOverview);
    }
}