namespace ZarqaPortal.Web.Core.Entities;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a course enrolled in a student's schedule.
/// </summary>
public class StudentSchedule
{
    /// <summary>
    /// The unique identifier for this schedule entry.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The username of the student who added this course.
    /// </summary>
    [Required]
    [MaxLength(150)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the enrolled course.
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Navigation property to the course.
    /// </summary>
    public Course? Course { get; set; }

    /// <summary>
    /// When the course was added to the schedule.
    /// </summary>
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
