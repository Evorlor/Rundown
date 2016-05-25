using UnityEngine;

public class Schedule
{
    [SerializeField]
    private Meeting[] meetings;

    /// <summary>
    /// Create a Schedule
    /// </summary>
    /// <param name="meetings">Meetings to initialize schedule with</param>
    public Schedule(Meeting[] meetings)
    {
        this.meetings = meetings;
    }

    /// <summary>
    /// Gets the meetings for this schedule
    /// </summary>
    /// <returns>The array of meetings</returns>
    public Meeting[] GetMeetings()
    {
        return meetings;
    }
}