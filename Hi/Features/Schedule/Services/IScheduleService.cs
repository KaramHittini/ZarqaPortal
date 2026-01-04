namespace ZarqaPortal.Web.Features.Schedule.Services;

using ZarqaPortal.Web.Core.Entities;

/// <summary>
/// Service interface for student schedule operations.
/// </summary>
public interface IScheduleService
{
    /// <summary>
    /// Gets all courses in a student's schedule.
    /// </summary>
    Task<IEnumerable<StudentSchedule>> GetScheduleForUserAsync(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a course to a student's schedule.
    /// </summary>
    Task<bool> AddCourseToScheduleAsync(string username, int courseId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a course is already in the student's schedule.
    /// </summary>
    Task<bool> IsCourseInScheduleAsync(string username, int courseId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a course from a student's schedule.
    /// </summary>
    Task<bool> RemoveCourseFromScheduleAsync(string username, int courseId, CancellationToken cancellationToken = default);
}
