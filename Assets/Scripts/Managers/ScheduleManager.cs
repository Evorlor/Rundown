using CustomUnityLibrary;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScheduleManager : ManagerBehaviour<ScheduleManager>
{
    private const string TitleError = "ERROR: The meeting must have a title!";
    private const string FilePath = "meetings.dat";

    private List<Meeting> meetings = new List<Meeting>();
    private Meeting activeMeeting;

    void Awake()
    {
        if (ES2.Exists(FilePath))
        {
            LoadMeetings();
        }
    }

    /// <summary>
    /// Saves all meetings to file
    /// </summary>
    public void SaveSchedule()
    {
        var schedule = new Schedule(meetings.ToArray());
        var json = JsonUtility.ToJson(schedule);
        ES2.Save(json, FilePath);
    }

    /// <summary>
    /// Loads all meetings from file into the meeting manager
    /// </summary>
    public void LoadMeetings()
    {
        var json = ES2.Load<string>(FilePath);
        var schedule = JsonUtility.FromJson<Schedule>(json);
        meetings = schedule.GetMeetings().ToList();
    }

    /// <summary>
    /// Adds a meeting to the list of meetings
    /// </summary>
    /// <param name="meeting">Meeting to be added to the list of meetings</param>
    public void AddMeeting(Meeting meeting)
    {
        if (!meetings.Contains(meeting))
        {
            meetings.Add(meeting);
        }
    }

    /// <summary>
    /// Removes the meeting from the list of meetings
    /// </summary>
    /// <param name="meeting">Meeting to be removed</param>
    /// <returns>Whether or not the removal was successful</returns>
    public bool RemoveMeeting(Meeting meeting)
    {
        return meetings.Remove(meeting);
    }

    /// <summary>
    /// Gets the currently active meeting
    /// </summary>
    /// <returns>The currently active meeting</returns>
    public Meeting GetActiveMeeting()
    {
        return activeMeeting;
    }

    /// <summary>
    /// Sets the currently active meeting
    /// </summary>
    /// <param name="meeting">The currently active meeting</param>
    public void SetActiveMeeting(Meeting meeting)
    {
        activeMeeting = meeting;
    }

    /// <summary>
    /// Gets a list of all of the meetings
    /// </summary>
    /// <returns>List of meetings</returns>
    public List<Meeting> GetMeetings()
    {
        return meetings;
    }
}
